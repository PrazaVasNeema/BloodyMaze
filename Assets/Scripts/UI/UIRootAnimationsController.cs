using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Controllers
{
    public class UIRootAnimationsController : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        public void FadeScreen()
        {
            m_animator.SetTrigger("Start");
        }

        public void UnfadeScreen()
        {
            m_animator.SetTrigger("End");
        }

        public void SetShowTutorialState(bool flagToSet)
        {
            m_animator.SetBool("ShouldShowTutorial", flagToSet);
        }
    }
}
