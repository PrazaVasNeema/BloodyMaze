using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class ActivateOnFlagCheck : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivate;
        [SerializeField] private string m_requiredEventKey;

        private void OnEnable()
        {
            GameEvents.OnEventFlagCheck += Activate;
        }

        private void OnDisable()
        {
            GameEvents.OnEventFlagCheck -= Activate;
        }

        private void Activate(string eventKey)
        {
            if (eventKey == m_requiredEventKey)
                onActivate?.Invoke();
        }
    }
}
