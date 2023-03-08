using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BloodyMaze.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        public enum MenuStates { INTRO, MAIN };

        [SerializeField] Animator m_animator;

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

        public void OnEnterGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void OnExitGame()
        {
            Application.Quit();
        }
    }
}
