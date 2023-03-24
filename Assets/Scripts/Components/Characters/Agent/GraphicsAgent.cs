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


        private void Awake()
        {
            // m_animator = GetComponent<Animator>();
            // m_animatorLocked = GetComponentsInChildren<Animator>()[1];
        }

        private void OnEnable()
        {
            transform.parent.GetComponent<HealthComponent>().OnChangeTargetLockStatus.AddListener(SetTargetIsBeingLocked);
        }

        private void OnDisable()
        {
            transform.parent.GetComponent<HealthComponent>().OnChangeTargetLockStatus.RemoveListener(SetTargetIsBeingLocked);
        }

        private void SetTargetIsBeingLocked(bool flag)
        {
            m_animatorLocked.SetBool("SetTargetIsBeingLocked", flag);
        }

        private void LateUpdate()
        {
            Debug.Log(Vector3.SqrMagnitude(transform.position - m_prevPosition));
            float moveSpeedDir = Vector3.SqrMagnitude(transform.position - m_prevPosition) > 0f ? 1f : 0f;
            // m_moveSpeed = Mathf.Lerp(m_moveSpeed, moveSpeedDir, Time.deltaTime);
            m_moveSpeed = Mathf.SmoothDamp(m_moveSpeed, moveSpeedDir, ref blendIdleFlyChangingSpeed, 0.5f, 1f);
            m_animator.SetFloat("MoveSpeed", m_moveSpeed);
            m_prevPosition = transform.position;

        }

    }
}
