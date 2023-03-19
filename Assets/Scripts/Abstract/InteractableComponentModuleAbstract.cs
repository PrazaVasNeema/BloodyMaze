using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public abstract class ActivateModuleAbstract : MonoBehaviour
    {
        [SerializeField] protected string[] m_eventsShouldBeChecked;
        public string[] eventsShouldBeChecked => m_eventsShouldBeChecked;
        [SerializeField] protected string[] m_eventsShouldBeUnchecked;
        public string[] eventsShouldBeUnhecked => m_eventsShouldBeUnchecked;
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
