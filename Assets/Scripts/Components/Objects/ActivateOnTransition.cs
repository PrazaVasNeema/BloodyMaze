using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class ActivateOnTransition : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnTransition;

        private void OnEnable()
        {
            GameEvents.OnTransition += Activate;
        }

        private void OnDisable()
        {
            GameEvents.OnTransition -= Activate;
        }

        public void Activate()
        {
            OnTransition?.Invoke();
        }
    }
}
