using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class AgentComponent : MonoBehaviour
    {
        [SerializeField] private float m_attackDistance = 2f;
        public float attackDistance => m_attackDistance;
        public Transform m_previousSeenAtTransform;
        public bool isStaggered;

        private void Awake()
        {
            m_previousSeenAtTransform = Instantiate(m_previousSeenAtTransform, transform.position, transform.rotation);

        }

        private void OnEnable()
        {
            GetComponent<HealthComponent>().onTakeDamage.AddListener(SetStaggeredStatus);
        }

        private void OnDisable()
        {
            GetComponent<HealthComponent>().onTakeDamage.RemoveListener(SetStaggeredStatus);
        }

        private void SetStaggeredStatus()
        {
            isStaggered = true;
        }
    }
}
