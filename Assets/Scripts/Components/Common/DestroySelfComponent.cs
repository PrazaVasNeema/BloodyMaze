using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class DestroySelfComponent : MonoBehaviour
    {
        [SerializeField] private float m_destroyDelay;
        public void DestroySelf()
        {
        }

        IEnumerator DestroySelfCo()
        {
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSecondsRealtime(m_destroyDelay);
            }
            Destroy(gameObject);
        }
    }
}
