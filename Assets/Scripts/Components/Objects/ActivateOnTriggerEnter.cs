using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze
{
    public class ActivateOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivate;

        private void OnTriggerEnter(Collider other)
        {
            if (ActionStatesManager.state == ActionStates.EXPLORING)
            {
                onActivate?.Invoke();
            }
        }
    }
}
