using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    public class UseAbilityComponentModuleBarrier : UseAbilityComponentModuleAbstract
    {
        [SerializeField] private float m_manaCost;
        [SerializeField] private bool m_shouldDrainManaOverTime;
        [SerializeField] private float m_drainingTime;

        private ManaComponent m_manaComponent;
        private IAbilityComponent m_abilityComponent;

        private void Awake()
        {
            m_manaComponent = transform.parent.transform.parent.GetComponent<ManaComponent>();
            m_abilityComponent = GetComponent<IAbilityComponent>();
        }

        public override bool Check()
        {
            bool canAttack = m_manaComponent.current >= m_manaCost;
            if (canAttack)
            {
                m_manaComponent.DrainAllOverTime(m_drainingTime);
                m_abilityComponent.UseAbility(m_drainingTime);
            }
            return canAttack;
        }
    }

}
