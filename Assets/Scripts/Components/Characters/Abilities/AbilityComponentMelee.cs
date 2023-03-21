using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class AbilityComponentMelee : MonoBehaviour, IAbilityComponent
    {
        [SerializeField] private float m_damage;
        [SerializeField] private LayerMask m_targetLayerMask;

        public void UseAbility(float optionalParameter)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (Collider hitCollider in hitColliders)
            {
                if (m_targetLayerMask.value == 1 << hitCollider.gameObject.layer
                 && hitCollider.GetComponent<HealthComponent>())
                {
                    hitCollider.GetComponent<HealthComponent>().ChangeHPWithAmount(m_damage);
                }
            }
        }
    }
}
