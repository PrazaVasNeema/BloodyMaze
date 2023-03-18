using System.Collections;
using UnityEngine;
using BloodyMaze.States;

namespace BloodyMaze.UI
{
    public class UIJournal : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_pages;
        [SerializeField] private Animator m_animator;

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
    }
}
