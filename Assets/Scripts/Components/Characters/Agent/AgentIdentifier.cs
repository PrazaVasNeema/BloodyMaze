using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class AgentIdentifier : MonoBehaviour
    {
        public int agentID;
        public UnityEvent<int> OnDead;

        private void OnEnable()
        {
            GetComponent<HealthComponent>().onDead.AddListener(CallOnDead);
        }

        private void CallOnDead()
        {
            Debug.Log("Lghgfhghfg");
            OnDead?.Invoke(agentID);
        }
    }
}
