using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(SphereCollider))]
    public class UseAbilityComponentModuleTargetedHitscan : UseAbilityComponentModuleAbstract
    {
        [SerializeField] private string m_ammoTypeName;
        [SerializeField] private float m_shootForce;
        [SerializeField] private LayerMask m_layersToTarget;
        private AmmunitionComponent m_ammunitionComponent;
        private IAbilityComponent m_abilityComponent;
        private SphereCollider m_sphereCollider;
        private HealthComponent m_currentTarget;
        public UnityEvent<GameObject> OnTargetLockedChanged;

        private void Awake()
        {
            m_ammunitionComponent = transform.parent.transform.parent.GetComponent<AmmunitionComponent>();
            m_abilityComponent = GetComponent<IAbilityComponent>();
            m_sphereCollider = GetComponent<SphereCollider>();
        }

        private void Update()
        {
            if (Time.frameCount % 10 == 0)
            {
                FindNewTarget();
            }
        }

        private void FindNewTarget()
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, m_sphereCollider.radius, m_layersToTarget);
            if (targets.Length > 0)
            {
                float minDistance = (transform.position - targets[0].transform.position).sqrMagnitude;
                HealthComponent m_targetHealthComponent = targets[0].GetComponentInParent<HealthComponent>();
                foreach (Collider target in targets)
                {
                    float curDistance = (transform.position - target.transform.position).sqrMagnitude;
                    if (curDistance < minDistance)
                    {
                        minDistance = curDistance;
                        m_targetHealthComponent = target.GetComponentInParent<HealthComponent>();
                    }
                }
                if (m_currentTarget)
                {
                    m_currentTarget.OnChangeTargetLockStatus?.Invoke(false);
                }
                m_currentTarget = m_targetHealthComponent;
                m_currentTarget.OnChangeTargetLockStatus?.Invoke(true);
                OnTargetLockedChanged?.Invoke(m_currentTarget.gameObject);
            }
            else
            {
                if (m_currentTarget)
                {
                    m_currentTarget.OnChangeTargetLockStatus?.Invoke(false);
                    OnTargetLockedChanged?.Invoke(null);
                }
                m_currentTarget = null;
            }
        }

        public override bool Check()
        {
            bool canAttack = m_currentTarget && m_ammunitionComponent.ShootAmmo(m_ammoTypeName) ? true : false;
            if (canAttack)
            {
                m_currentTarget.TakeDamage(10);
            }
            return canAttack;
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}
