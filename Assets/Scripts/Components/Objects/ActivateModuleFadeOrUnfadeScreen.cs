using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class ActivateModuleFadeOrUnfadeScreen : MonoBehaviour
    {
        [SerializeField] private bool m_shouldFadeScreen;
        public void FadeOrUnfadeScreen()
        {
            if (m_shouldFadeScreen)
            {
                FindAnyObjectByType<UIRootAnimationsController>().FadeScreenExtra();
            }
            else
            {
                FindAnyObjectByType<UIRootAnimationsController>().UnfadeScreenExtra();
            }
        }

        public void FadeOrUnfadeScreenNotExtra()
        {
            if (m_shouldFadeScreen)
            {
                FindAnyObjectByType<UIRootAnimationsController>().FadeScreen();
            }
            else
            {
                FindAnyObjectByType<UIRootAnimationsController>().UnfadeScreen();
            }
        }
    }
}
