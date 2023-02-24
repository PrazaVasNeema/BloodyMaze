using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class UseAbilityComponentModuleRevolver : UseAbilityComponentModuleAbstract
    {
        private AmmunitionComponent m_ammunitionComponent;

        private void Awake()
        {
            m_ammunitionComponent = transform.parent.GetComponent<AmmunitionComponent>();
        }

        public override bool Check()
        {
            return m_ammunitionComponent.ShootAmmo();
        }
    }
}
