using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    [RequireComponent(typeof(Collider))]
    public class BarrierComponent : SpawnableComponentAbstract
    {
        [SerializeField] private LayerMask m_shouldBeAffected;
        [SerializeField] private float m_damage = 0f;

        private float m_lifeTime = 2f;
        private List<int> m_affectedAgentsIds = new();

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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.transform.name);
            var health = other.gameObject.GetComponentInParent<HealthComponent>();
            if (health && m_shouldBeAffected.value == 1 << other.gameObject.layer
            && !m_affectedAgentsIds.Contains(other.gameObject.GetComponentInParent<AgentIdentifier>().agentID))
            {
                Debug.Log($"Triggered: {other.name}");
                m_affectedAgentsIds.Add(other.gameObject.GetComponentInParent<AgentIdentifier>().agentID);
                health.ChangeHPWithAmount(m_damage);
            }
        }
    }

}
