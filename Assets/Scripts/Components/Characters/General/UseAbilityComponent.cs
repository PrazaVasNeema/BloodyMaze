using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    public class UseAbilityComponent : MonoBehaviour
    {
        [SerializeField] private float m_cooldownTime = 0.1f;
        public float cooldownTime => m_cooldownTime;
        private float m_timer = 0;
        private IAbilityComponent m_abilityComponent;
        private UseAbilityComponentModuleAbstract m_useAbilityComponentModule;

        private void Awake()
        {
            m_abilityComponent = GetComponent<IAbilityComponent>();
            m_useAbilityComponentModule = GetComponent<UseAbilityComponentModuleAbstract>();
            m_timer = 0;
        }

        public void Init()
        {

        }

        public bool UseAbility()
        {
            if (m_timer <= 0f)
            {
                if (m_useAbilityComponentModule != null)
                {
                    if (m_useAbilityComponentModule.Check())
                        m_timer = m_cooldownTime;
                }
                else
                {
                    m_abilityComponent.UseAbility(-1);
                    m_timer = m_cooldownTime;
                }
                return true;
            }
            return false;
        }

        private void Update()
        {
            if (m_timer > 0f)
            {
                m_timer -= Time.deltaTime;
            }
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

    }

}
