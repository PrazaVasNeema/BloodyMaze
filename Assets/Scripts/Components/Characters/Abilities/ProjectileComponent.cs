using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ProjectileComponent : SpawnableComponentAbstract
    {
        [SerializeField] private LayerMask m_shouldBeBlockedBy;
        [SerializeField] private LayerMask m_shouldIgnore;
        private Rigidbody m_body;
        private float m_damage = 1f;
        private float m_force = 10f;


        private void Awake()
        {
            m_body = GetComponent<Rigidbody>();
        }

        public override void Activate(float damage, float force)
        {
            m_damage = damage;
            m_force = force == -1 ? m_force : force;
            m_body.AddForce(transform.forward * m_force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            HealthComponent health = other.gameObject.GetComponentInParent<HealthComponent>();
            bool shouldIgnoreThisCollision = m_shouldIgnore.value == 1 << other.gameObject.layer;
            if (health && !(m_shouldBeBlockedBy.value == 1 << other.gameObject.layer)
            && !shouldIgnoreThisCollision)
            {
                health.ChangeHPWithAmount(m_damage);
            }
            if (!shouldIgnoreThisCollision)
                Destroy(gameObject);
        }

    }

}
