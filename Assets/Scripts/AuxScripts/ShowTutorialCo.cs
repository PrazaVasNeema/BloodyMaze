using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class ShowTutorialCo : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        public void ShowTutorial()
        {
            m_animator.SetBool("ShouldShowTutorial", true);
        }

        public void HideTutorial()
        {
            m_animator.SetBool("ShouldShowTutorial", false);
        }

        // IEnumerator ShowTutorialCo()
        // {

        // }
    }
}
