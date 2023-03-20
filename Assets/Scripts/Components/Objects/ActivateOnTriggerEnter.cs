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
            if (LayerMask.NameToLayer("Player") == 1 << other.gameObject.layer
             && ActionStatesManager.current.state == ActionStates.EXPLORING)
            {
                onActivate?.Invoke();
            }
        }
    }
}
