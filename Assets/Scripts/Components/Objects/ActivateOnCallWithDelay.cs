using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze
{
    public class ActivateOnCallWithDelay : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivate;
        [SerializeField] private float m_delay = 2f;

        public void Activate()
        {
            StartCoroutine(ActivateWithDelayCo());
        }

        IEnumerator ActivateWithDelayCo()
        {
            yield return new WaitForSecondsRealtime(2f);
            onActivate?.Invoke();
        }
    }
}
