using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public abstract class ActivateModuleAbstract : MonoBehaviour
    {

        [SerializeField] protected string m_eventFlagToCheck;
        public string eventFlagCheck => m_eventFlagToCheck;


        protected bool m_flag;


        private void OnEnable()
        {
            GameEvents.OnStateChanged += SetFlagToTrue;
        }

        private void OnDisable()
        {
            GameEvents.OnStateChanged -= SetFlagToTrue;
        }

        private void SetFlagToTrue()
        {
            m_flag = true;
        }

        public abstract void ActivateModule();
    }
}
