using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class ActivateModuleShowOrHideTutorial : MonoBehaviour
    {
        [SerializeField] private bool m_shouldShowTutorial;

        public void ShowOrHideTutorial()
        {
            FindAnyObjectByType<UIRootAnimationsController>().SetShowTutorialState(m_shouldShowTutorial ? true : false);
        }
    }
}
