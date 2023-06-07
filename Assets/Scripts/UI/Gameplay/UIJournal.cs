using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.States;
using TMPro;

namespace BloodyMaze.UI
{
    public class UIJournal : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_pages;
        [SerializeField] private Animator m_animator;
        [SerializeField] private TMP_Text m_currentObjectiveTextField;
        [SerializeField] private TMP_Text m_objectivesScrollviewTextField;
        [SerializeField] private AudioSource m_audioSource;

        // Objective tracking system
        [SerializeField] private AudioClip m_ObjectTrackingSound;
        private List<GlobalEventsData> m_globalEventsData;
        private int m_currentEventFlagIndex;
        private bool m_initIsComplete;
        //
        private int m_currentPageIndex;

        private void Awake()
        {
            foreach (GameObject page in m_pages)
                page.SetActive(false);
        }

        private void OnEnable()
        {
            Open();
        }

        public void Open()
        {
            StartCoroutine(OpenCo());
        }

        public void ChangePage(bool shouldBeNext)
        {
            Debug.Log("ChangePage");
            StartCoroutine(ChangePageCo(shouldBeNext));
        }

        public void Close()
        {
            StartCoroutine(CloseCo());
        }

        IEnumerator OpenCo()
        {
            ActionStatesManager.SetState(ActionStates.INTERACTING);
            foreach (GameObject page in m_pages)
                page.SetActive(false);
            m_animator.SetTrigger("Open");

            bool doOnce = true;
            if (doOnce)
            {
                doOnce = false;
                yield return new WaitForSecondsRealtime(1f);
            }
            m_pages[m_currentPageIndex].SetActive(true);
        }

        IEnumerator ChangePageCo(bool shouldBeNext)
        {
            m_pages[m_currentPageIndex].SetActive(false);
            m_animator.SetTrigger(shouldBeNext ? "NextPage" : "PrevPage");

            bool doOnce = true;
            if (doOnce)
            {
                doOnce = false;
                yield return new WaitForSecondsRealtime(1f);
            }
            m_currentPageIndex += shouldBeNext ? 1 : -1;
            m_pages[m_currentPageIndex].SetActive(true);
        }

        IEnumerator CloseCo()
        {
            ActionStatesManager.ChangeState();
            m_pages[m_currentPageIndex].SetActive(false);
            m_animator.SetTrigger("Close");
            bool doOnce = true;
            if (doOnce)
            {
                doOnce = false;
                yield return new WaitForSecondsRealtime(1f);
            }
            GameEvents.OnCallGotoFunction("gameplay");
        }


        public void InitObjectiveTracking()
        {
            m_globalEventsData = GameController.instance.playerProfile.playerProfileData.globalEventsData;
            m_currentEventFlagIndex = -1;
            m_initIsComplete = false;
            FillAllObjectives();
            GameEvents.OnEventFlagCheck += FillObjective;
            m_initIsComplete = true;
        }

        public void DeInitObjectiveTracking()
        {
            GameEvents.OnEventFlagCheck -= FillObjective;
            UpdateObjective();
        }

        private void FillAllObjectives()
        {
            Debug.Log("FillFields");
            bool endReached = false;
            for (; m_currentEventFlagIndex < m_globalEventsData.Count;)
            {
                if (endReached)
                {
                    break;
                }
                m_currentEventFlagIndex++;
                if (!m_globalEventsData[m_currentEventFlagIndex].flag)
                {
                    endReached = true;
                }
                if (m_currentEventFlagIndex >= m_globalEventsData.Count)
                {
                    Debug.LogError("Events set up is now right: the end is reached");
                    break;
                }
                FillObjective(m_globalEventsData[m_currentEventFlagIndex].eventKey);

            }
        }


        private void FillObjective(string correspondingEventKey)
        {
            Debug.Log(m_globalEventsData[m_currentEventFlagIndex].eventKey);
            Debug.Log(correspondingEventKey);
            if (m_globalEventsData[m_currentEventFlagIndex].eventKey != correspondingEventKey)
                return;
            if (m_initIsComplete)
                m_currentEventFlagIndex++;
            while (string.IsNullOrEmpty(m_globalEventsData[m_currentEventFlagIndex].objectiveText))
            {
                m_currentEventFlagIndex++;
                if (m_currentEventFlagIndex >= m_globalEventsData.Count)
                {
                    Debug.LogError("Events set up is now right: the end is reached");
                    break;
                }
                continue;
            }
            if (m_initIsComplete)
                CoroutinesInDemandHub.instance.WaitForExploringState(this, m_globalEventsData[m_currentEventFlagIndex].objectiveText);
            else
                UpdateObjective(m_globalEventsData[m_currentEventFlagIndex].objectiveText);

        }

        IEnumerator WaitForExploringState(string newObjectiveText = null)
        {
            while (ActionStatesManager.state != ActionStates.EXPLORING)
            {
                yield return new WaitForSecondsRealtime(0.1f);
            }
            UpdateObjective(newObjectiveText);
        }

        /// <summary>
        /// Leave param empty to clear all fields
        /// </summary>
        public void UpdateObjective(string newObjectiveText = null)
        {
            if (string.IsNullOrEmpty(newObjectiveText))
            {
                m_currentObjectiveTextField.text = "";
                m_objectivesScrollviewTextField.text = "";
            }
            else
            {
                m_currentObjectiveTextField.text = newObjectiveText;
                List<string> objectivesScrollviewTextParts = new(m_objectivesScrollviewTextField.text.Split('\n'));
                objectivesScrollviewTextParts[0] = $"<s>{objectivesScrollviewTextParts[0]}</s>";
                string newObjectivesScrollviewText = $"{newObjectiveText}\n";
                foreach (string part in objectivesScrollviewTextParts)
                    newObjectivesScrollviewText += $"\n{part}";
                m_objectivesScrollviewTextField.text = newObjectivesScrollviewText;
            }
            if (m_initIsComplete)
            {
                GameEvents.OnShowMiniMessage?.Invoke("new_diary_entry");
                m_audioSource.clip = m_ObjectTrackingSound;
                m_audioSource.Play();
            }
        }
    }
}
