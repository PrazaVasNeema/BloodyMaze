using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class ActivateOnTransition : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnScreenIsBlackened;

        private void OnEnable()
        {
            GameEvents.OnScreenBlacken += Activate;
        }

        private void OnDisable()
        {
            GameEvents.OnScreenBlacken -= Activate;
        }

        public void Activate()
        {
            OnScreenIsBlackened?.Invoke();
        }
    }
}
