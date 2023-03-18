using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public abstract class ActivateModuleAbstract : MonoBehaviour
    {
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
