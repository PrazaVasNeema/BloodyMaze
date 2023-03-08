using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Controllers
{
    public class UIMenusController : MonoBehaviour
    {
        public static UIMenusController current { private set; get; }

        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        [SerializeField] private GameObject[] m_menusPages;
        [SerializeField] private Animator m_animator;

        public void OpenMenus()
        {
            GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            GameEvents.OnSetInteractState?.Invoke();
            StartCoroutine(OpenMenusCo());
        }

        public void CloseMenus()
        {
            GameEvents.OnSetInteractState?.Invoke();
            StartCoroutine(CloseMenusCo());
        }

        public void ChangePage(bool isNextPage)
        {
            StartCoroutine(ChangePageCo(isNextPage));
        }

        private IEnumerator OpenMenusCo()
        {
            m_animator.SetTrigger("Open");
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(2f);
            }
            m_menusPages[0].SetActive(true);
        }

        private IEnumerator CloseMenusCo()
        {
            m_menusPages[0].SetActive(false);
            m_animator.SetTrigger("Close");
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(2f);
            }
        }

        private IEnumerator ChangePageCo(bool isNextPage)
        {
            m_menusPages[0].SetActive(false);
            if (isNextPage)
            {
                m_animator.SetTrigger("NextPage");
            }
            else
            {
                m_animator.SetTrigger("PrevPage");
            }
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(2f);
            }
            m_menusPages[0].SetActive(true);
        }
    }
}
