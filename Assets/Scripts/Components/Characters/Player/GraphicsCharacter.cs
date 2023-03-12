using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class GraphicsCharacter : MonoBehaviour
    {
        private Animator m_animator;
        private AbilitiesManager m_abilitiesManager;
        private MovementComponentCharacter m_movementComponentCharacter;

        static int SpeedMoveId = Animator.StringToHash("MoveSpeed");
        static int ShootRoundId = Animator.StringToHash("ShootRound");

        private Vector3 m_lastPosition;


        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_abilitiesManager = GetComponentInParent<AbilitiesManager>();
            m_movementComponentCharacter = GetComponentInParent<MovementComponentCharacter>();
        }

        private void OnEnable()
        {
            m_abilitiesManager.onUseAbility.AddListener(OnShootRoundHandler);
        }

        private void OnDisable()
        {
            m_abilitiesManager.onUseAbility.RemoveListener(OnShootRoundHandler);
        }

        private void OnShootRoundHandler()
        {
            Debug.Log("CheckInvoke");
            m_animator.SetTrigger(ShootRoundId);
        }

        private void LateUpdate()
        {
            var velocity = Mathf.Clamp01(m_movementComponentCharacter.velocity / m_movementComponentCharacter.speed);
            m_animator.SetFloat(SpeedMoveId, velocity);
        }
    }
}
