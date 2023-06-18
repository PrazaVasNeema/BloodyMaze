using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class AgentIdentifier : MonoBehaviour
    {
        [SerializeField] private string m_eventToCheckOnDeath;
        public int agentID;
        public UnityEvent<int> OnDead;

        private void OnEnable()
        {
            GetComponent<HealthComponent>().onDead.AddListener(CallOnDead);
        }

        private void OnDisable()
        {
            GetComponent<HealthComponent>().onDead.RemoveListener(CallOnDead);
        }

        private void CallOnDead()
        {
            GameEvents.OnEventFlagCheck?.Invoke(m_eventToCheckOnDeath);
            Debug.Log("Lghgfhghfg");
            OnDead?.Invoke(agentID);
        }
    }
}
