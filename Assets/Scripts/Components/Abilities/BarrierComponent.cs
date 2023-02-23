using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    [RequireComponent(typeof(Collider))]
    public class BarrierComponent : MonoBehaviour, ISpawnableComponent
    {
        private float m_damage = 0f;
        public void Activate(float damage, float force)
        {
            m_damage = damage;
        }

        private void OnCollisionEnter(Collision other)
        {
            // TODO работа с коллизией - урон и отбрасывание
            // var health = other.gameObject.GetComponentInParent<HealthComponent>();
            // if (health)
            // {
            //     health.TakeDamage(m_damage);
            // }

            Destroy(gameObject);
        }
    }

}
