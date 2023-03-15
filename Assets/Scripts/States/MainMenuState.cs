using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public class MainMenuState : GameStateBehavior
    {
        public enum MenuStates { INTRO, MAIN };

        [SerializeField] private Animator m_animator;
        private MenuStates m_state = MenuStates.INTRO;
        public MenuStates state => m_state;

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

        public void LoadLevel()
        {
            GameController.LoadScene("SampleScene");
        }

        public void OnExitGame()
        {
            Application.Quit();
        }
    }
}
