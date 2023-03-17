using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class OnTransitionAct : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivate;

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
            onActivate?.Invoke();
        }
    }
}
