using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public abstract class InteractableComponentModuleAbstract : MonoBehaviour
    {
        [SerializeField] private string m_messageToShow;
        public string messageToShow => m_messageToShow;
        protected bool m_flag;

        private void OnEnable()
        {
            GameEvents.OnGameplayStateChanged += SetFlagToTrue;
        }

        private void OnDisable()
        {
            GameEvents.OnGameplayStateChanged -= SetFlagToTrue;
        }

        private void SetFlagToTrue()
        {
            m_flag = true;
        }

        public abstract void ActivateModule();
    }
}
