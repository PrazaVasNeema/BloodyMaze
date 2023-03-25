using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze
{
    public class ActivateOnAgentDeath : MonoBehaviour
    {
        public UnityEvent OnAgentDeath;

        public void Activate()
        {
            OnAgentDeath?.Invoke();
        }
    }
}
