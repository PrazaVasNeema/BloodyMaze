using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class UseAbilityComponentMedkit : UseAbilityComponentModuleAbstract
    {
        private MedsComponent m_medsComponent;
        private HealthComponent m_healthComponent;

        private void Awake()
        {
            m_medsComponent = transform.parent.transform.parent.GetComponent<MedsComponent>();
            m_healthComponent = transform.parent.transform.parent.GetComponent<HealthComponent>();
        }

        public override bool Check()
        {
            if (m_healthComponent.currentHealth != m_healthComponent.maxHealth && m_medsComponent.UseMeds())
            {
                m_healthComponent.ChangeHPWithAmount(-m_medsComponent.meds.hpToHeal);
                return true;
            }
            else
                return false;
        }

    }
}
