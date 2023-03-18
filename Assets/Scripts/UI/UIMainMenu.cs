using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.UI
{
    [RequireComponent(typeof(Animator))]
    public class UIMainMenu : UIPanelBaseAbstract
    {
        public enum MenuStates { INTRO, MAIN };

        private Animator m_animator;
        private MenuStates m_state = MenuStates.INTRO;
        public MenuStates state => m_state;

        private void Awake()
        {
            TryGetComponent(out m_animator);
        }

        public void ChangeState()
        {
            switch (m_state)
            {
                case MenuStates.INTRO:
                    m_state = MenuStates.MAIN;
                    m_animator.SetBool("AppearAndDisappear", false);
                    m_animator.SetTrigger("ZoomIn");
                    break;
                case MenuStates.MAIN:
                    m_state = MenuStates.INTRO;
                    m_animator.SetBool("AppearAndDisappear", true);
                    m_animator.SetTrigger("ZoomOut");
                    break;
            }
        }

        public void OnEnterGame()
        {
            GameController.LoadScene("SampleScene");
        }

        public void OnExitGame()
        {
            Application.Quit();
        }
    }
}
