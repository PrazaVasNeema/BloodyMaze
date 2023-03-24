using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class AgentComponent : MonoBehaviour
    {
        [SerializeField] private float m_attackDistance = 2f;
        public float attackDistance => m_attackDistance;
        public Transform m_previousSeenAtTransform;
        public bool isStaggered;
        public UnityEvent onAttack;
        public UnityEvent onLookForTarget;
        public UnityEvent<bool> OnSetIsTriggeredStatus;
        public UnityEvent OnAlert;


        private void Awake()
        {
            m_previousSeenAtTransform = Instantiate(m_previousSeenAtTransform, transform.position, transform.rotation);

        }

        private void OnEnable()
        {
            GetComponent<HealthComponent>().onTakeDamage.AddListener(SetStaggeredStatus);
            GetComponent<AbilitiesManager>().onUseAbility.AddListener(onAttack.Invoke);
        }

        private void OnDisable()
        {
            GetComponent<HealthComponent>().onTakeDamage.RemoveListener(SetStaggeredStatus);
            GetComponent<AbilitiesManager>().onUseAbility.RemoveListener(onAttack.Invoke);
        }

        private void SetStaggeredStatus()
        {
            isStaggered = true;
        }
    }
}
