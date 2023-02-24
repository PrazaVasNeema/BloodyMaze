using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class UseAbilityComponentModuleRevolver : UseAbilityComponentModuleAbstract
    {
        [SerializeField] private string m_ammoTypeName;
        private AmmunitionComponent m_ammunitionComponent;

        private void Awake()
        {
            m_ammunitionComponent = transform.parent.transform.parent.GetComponent<AmmunitionComponent>();
        }

        public override bool Check()
        {
            return m_ammunitionComponent.ShootAmmo(m_ammoTypeName);
        }
    }
}
