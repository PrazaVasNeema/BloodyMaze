using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class GraphicsAgent : MonoBehaviour
    {
        private Animator m_animator;
        private Animator m_animatorLocked;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_animatorLocked = GetComponentsInChildren<Animator>()[1];
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
    }
}
