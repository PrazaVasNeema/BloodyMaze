using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    public class ManaComponent : MonoBehaviour
    {
        [SerializeField] private float m_current = 100f;
        public float current => m_current;
        [SerializeField] private float m_maxValue = 100f;
        [SerializeField] private float m_speedAllRestore = 10f;
        public float percent => m_current / m_maxValue;

        private bool m_manaIsBeingDrained;
        private float m_drainingTime;

        public void Init(float maxValue, float speedRestore)
        {
            m_current = m_maxValue = maxValue;
            m_speedAllRestore = speedRestore;
        }

        public void Reduce(float value)
        {
            m_current -= value;
        }

        public void DrainAllOverTime(float drainingTime)
        {
            m_manaIsBeingDrained = true;
            m_drainingTime = drainingTime;
        }

        private void Update()
        {

            var offset = m_manaIsBeingDrained ? -(m_maxValue / m_drainingTime) * Time.deltaTime :
             (m_maxValue / m_speedAllRestore) * Time.deltaTime;
            m_current = Mathf.Clamp(m_current + offset, 0, m_maxValue);
            if (m_current == 0)
                m_manaIsBeingDrained = false;
        }
    }

}
