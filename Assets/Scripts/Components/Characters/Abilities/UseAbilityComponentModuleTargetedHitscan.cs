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
        public UnityEvent OnShootRound;
        public UnityEvent OnDrumIsEmpty;

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
                float minDistance = 10000f;
                HealthComponent m_targetHealthComponent = null;
                foreach (Collider target in targets)
                {
                    if (target.gameObject.active == false)
                        break;
                    float curDistance = (transform.position - target.transform.position).sqrMagnitude;
                    if (curDistance < minDistance)
                    {
                        minDistance = curDistance;
                        m_targetHealthComponent = target.GetComponentInParent<HealthComponent>();
                    }
                }
                if (m_targetHealthComponent == null)
                    return;
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
                m_currentTarget.ChangeHPWithAmount(50);
                OnShootRound?.Invoke();
            }
            else
                OnDrumIsEmpty?.Invoke();
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
