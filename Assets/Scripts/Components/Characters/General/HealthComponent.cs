using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float m_currentHealth = 100;
        [SerializeField] private float m_maxHealth = 100;

        public float currentHealth => m_currentHealth;

        public bool isDead => m_currentHealth <= 0;

        public float percent => m_currentHealth / m_maxHealth;

        public UnityEvent onDead;

        public void Init(float currentHealth, float maxHealth)
        {
            m_currentHealth = currentHealth;
            m_maxHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            m_currentHealth = Mathf.Max(m_currentHealth - damage, 0f);

            if (m_currentHealth == 0)
            {
                onDead.Invoke();
                // Destroy(gameObject);
            }
        }
    }
}
