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
        [SerializeField] private float m_restoringRate = 30f;
        public float percent => m_current / m_maxValue;

        private bool m_manaIsBeingDrained;
        private float m_drainingTime;

        public void Init(float currentMana, float maxValue, float manaRestoringRate)
        {
            m_current = currentMana;
            m_maxValue = maxValue;
            m_restoringRate = manaRestoringRate;
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

            var offset = m_manaIsBeingDrained ? -m_restoringRate * Time.deltaTime :
             m_restoringRate * Time.deltaTime;
            m_current = Mathf.Clamp(m_current + offset, 0, m_maxValue);
            if (m_current == 0)
                m_manaIsBeingDrained = false;
        }
    }

}
