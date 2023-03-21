using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    [RequireComponent(typeof(Collider))]
    public class BarrierComponent : SpawnableComponentAbstract
    {
        private float m_damage = 0f;
        private float m_lifeTime = 2f;

        public override void Activate(float damage, float lifeTime)
        {
            m_damage = damage;
            m_lifeTime = lifeTime == -1 ? m_lifeTime : lifeTime;
        }

        private void Update()
        {
            m_lifeTime -= Time.deltaTime;
            if (m_lifeTime <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            var health = other.gameObject.GetComponentInParent<HealthComponent>();
            if (health)
            {
                health.ChangeHPWithAmount(m_damage);
            }
        }
    }

}
