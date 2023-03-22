using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float m_currentHealth = 100;
        public float currentHealth => m_currentHealth;
        [SerializeField] private float m_maxHealth = 100;
        public float maxHealth => m_maxHealth;


        public bool isDead => m_currentHealth <= 0;

        public float percent => m_currentHealth / m_maxHealth;

        public UnityEvent onDead;
        public UnityEvent onTakeDamage;
        public UnityEvent<bool> OnChangeTargetLockStatus;

        public void Init(float currentHealth, float maxHealth)
        {
            m_currentHealth = currentHealth;
            m_maxHealth = maxHealth;
        }

        public void ChangeHPWithAmount(float amount)
        {
            m_currentHealth = amount >= 0 ? Mathf.Max(m_currentHealth - amount, 0f) : Mathf.Min(m_currentHealth - amount, 100f);
            if (amount > 0)
                onTakeDamage?.Invoke();
            if (m_currentHealth == 0)
            {
                onDead.Invoke();
                if (GetComponent<CharacterComponent>())
                    GameEvents.OnPCDeath?.Invoke();
                // Destroy(gameObject);
            }
        }
    }
}
