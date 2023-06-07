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

        public void UpdateObjectives(string newObjectiveText)
        {
            m_currentObjectiveTextField.text = newObjectiveText;
            List<string> objectivesScrollviewTextParts = new(m_objectivesScrollviewTextField.text.Split('\n'));
            objectivesScrollviewTextParts[0] = $"<s>{objectivesScrollviewTextParts[0]}</s>";
            string newObjectivesScrollviewText = $"{newObjectiveText}\n";
            foreach (string part in objectivesScrollviewTextParts)
                newObjectivesScrollviewText += $"\n{part}";
            m_objectivesScrollviewTextField.text = newObjectivesScrollviewText;
        }
    }
}
