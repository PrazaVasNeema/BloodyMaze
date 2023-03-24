using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class GraphicsAgent : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private Animator m_animatorLocked;
        private Vector3 m_prevPosition;
        private float m_moveSpeed;
        protected float blendIdleFlyChangingSpeed = 0f;
        private bool m_lostTarget;


        private void Awake()
        {
            // m_animator = GetComponent<Animator>();
            // m_animatorLocked = GetComponentsInChildren<Animator>()[1];
        }

        private void OnEnable()
        {
            HealthComponent healthComponent = transform.parent.GetComponent<HealthComponent>();
            AgentComponent agentComponent = transform.parent.GetComponent<AgentComponent>();
            healthComponent.OnChangeTargetLockStatus.AddListener(SetTargetIsBeingLocked);
            healthComponent.onTakeDamage.AddListener(SetStaggerTrigger);
            healthComponent.onDead.AddListener(SetDieTrigger);
            agentComponent.onAttack.AddListener(SetAttackTrigger);
            agentComponent.onLookForTarget.AddListener(SetLookForTargetTrigger);
            agentComponent.OnSetLostTargetStatus.AddListener(SetLostTargetValue);
            agentComponent.OnAlert.AddListener(SetAlertTrigger);
        }

        private void OnDisable()
        {
            HealthComponent healthComponent = transform.parent.GetComponent<HealthComponent>();
            AgentComponent agentComponent = transform.parent.GetComponent<AgentComponent>();
            healthComponent.OnChangeTargetLockStatus.RemoveListener(SetTargetIsBeingLocked);
            healthComponent.onTakeDamage.RemoveListener(SetStaggerTrigger);
            healthComponent.onDead.RemoveListener(SetDieTrigger);
            agentComponent.onAttack.RemoveListener(SetAttackTrigger);
            agentComponent.onLookForTarget.RemoveListener(SetLookForTargetTrigger);
            agentComponent.OnSetLostTargetStatus.RemoveListener(SetLostTargetValue);
            agentComponent.OnAlert.RemoveListener(SetAlertTrigger);
        }

        private void SetTargetIsBeingLocked(bool flag)
        {
            m_animatorLocked.SetBool("SetTargetIsBeingLocked", flag);
        }

        private void SetStaggerTrigger()
        {
            m_animator.SetTrigger("Stagger");
        }

        private void SetDieTrigger()
        {
            m_animator.SetTrigger("Die");
        }

        private void SetAttackTrigger()
        {
            m_animator.SetTrigger("Attack");
        }

        private void SetLookForTargetTrigger()
        {
            m_animator.SetTrigger("LookForTarget");
        }

        private void SetLostTargetValue(bool lostTarget)
        {
            m_lostTarget = lostTarget;
        }

        private void SetAlertTrigger()
        {
            m_animator.SetTrigger("Alert");
        }

        private void LateUpdate()
        {
            float moveSpeedDir = Vector3.SqrMagnitude(transform.position - m_prevPosition) > 0f ? 1f : 0f;
            // m_moveSpeed = Mathf.Lerp(m_moveSpeed, moveSpeedDir, Time.deltaTime);
            m_moveSpeed = Mathf.SmoothDamp(m_moveSpeed, moveSpeedDir, ref blendIdleFlyChangingSpeed, 0.5f, 1f);
            m_animator.SetFloat("MoveSpeed", m_moveSpeed);
            m_prevPosition = transform.position;

        }

    }
}
