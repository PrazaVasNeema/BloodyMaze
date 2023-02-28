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
            if (health && !(m_shouldBeBlockedBy.value == 1 << other.gameObject.layer))
            {
                health.TakeDamage(m_damage);
            }

            Destroy(gameObject);
        }

    }

}
