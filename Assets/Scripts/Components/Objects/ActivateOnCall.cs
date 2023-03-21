using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(BoxCollider))]
    public class ActivateOnCall : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivate;

        public void Activate()
        {
            onActivate?.Invoke();
        }
    }
}
