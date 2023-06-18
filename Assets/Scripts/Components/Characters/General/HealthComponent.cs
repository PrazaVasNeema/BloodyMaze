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
        [SerializeField] private GameObject m_objectToSetUnactiveOnDeath;


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
            onTakeDamage?.Invoke();
            if (m_currentHealth == 0)
            {
                if (m_objectToSetUnactiveOnDeath != null)
                    m_objectToSetUnactiveOnDeath.SetActive(false);
                onDead?.Invoke();
                if (GetComponent<CharacterComponent>())
                    GameEvents.OnPCDeath?.Invoke();
                OnChangeTargetLockStatus.Invoke(false);
                Destroy(this);
                // Destroy(gameObject);
            }
        }
    }
}
