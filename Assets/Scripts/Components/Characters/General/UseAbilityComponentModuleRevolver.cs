using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class UseAbilityComponentModuleRevolver : UseAbilityComponentModuleAbstract
    {
        [SerializeField] private string m_ammoTypeName;
        [SerializeField] private float m_shootForce;
        private AmmunitionComponent m_ammunitionComponent;
        private IAbilityComponent m_abilityComponent;


        private void Awake()
        {
            m_ammunitionComponent = transform.parent.transform.parent.GetComponent<AmmunitionComponent>();
            m_abilityComponent = GetComponent<IAbilityComponent>();
        }

        public override bool Check()
        {
            bool canAttack = m_ammunitionComponent.ShootAmmo(m_ammoTypeName);
            if (canAttack)
            {
                m_abilityComponent.UseAbility(m_shootForce);
            }
            return canAttack;
        }
    }
}
