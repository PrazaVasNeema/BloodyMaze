using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ProjectileComponent : SpawnableComponentAbstract
    {
        private Rigidbody m_body;
        private float m_damage = 1f;
        private float m_force = 5f;


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
            // TODO работа с коллизией
            // var health = other.gameObject.GetComponentInParent<HealthComponent>();
            // if (health)
            // {
            //     health.TakeDamage(m_damage);
            // }

            Destroy(gameObject);
        }

    }

}
