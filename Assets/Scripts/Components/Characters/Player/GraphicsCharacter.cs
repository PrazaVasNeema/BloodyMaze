using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class GraphicsCharacter : MonoBehaviour
    {
        [SerializeField] private Transform m_manSpine;
        [SerializeField] private UseAbilityComponentModuleTargetedHitscan m_ability;
        private Animator m_animator;
        private AbilitiesManager m_abilitiesManager;
        private MovementComponentCharacter m_movementComponentCharacter;

        static int SpeedMoveId = Animator.StringToHash("MoveSpeed");
        static int ShootRoundId = Animator.StringToHash("ShootRound");

        private Vector3 m_lastPosition;
        private GameObject m_currentTarget;


        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_abilitiesManager = GetComponentInParent<AbilitiesManager>();
            m_movementComponentCharacter = GetComponentInParent<MovementComponentCharacter>();
        }

        private void OnEnable()
        {
            m_abilitiesManager.onUseAbility.AddListener(OnShootRoundHandler);
            m_ability.OnTargetLockedChanged.AddListener(SetCurrentTarget);
        }

        private void OnDisable()
        {
            m_abilitiesManager.onUseAbility.RemoveListener(OnShootRoundHandler);
            m_ability.OnTargetLockedChanged.RemoveListener(SetCurrentTarget);
        }

        private void SetCurrentTarget(GameObject target)
        {
            m_currentTarget = target;
            if (m_currentTarget == null)
            {
                m_manSpine.LookAt(gameObject.transform.forward);
            }
        }

        private void OnShootRoundHandler()
        {
            Debug.Log("CheckInvoke");
            m_animator.SetTrigger(ShootRoundId);
        }

        private void LateUpdate()
        {
            // transform.LookAt(new Vector3(1, transform.position.y, 1));

            var velocity = Mathf.Clamp01(m_movementComponentCharacter.velocity / m_movementComponentCharacter.speed);
            m_animator.SetFloat(SpeedMoveId, velocity);
            // m_manSpine.LookAt(Vector3.Normalize(new Vector3(gameObject.transform.forward.x, gameObject.transform.forward.y, gameObject.transform.forward.z)));
            if (m_currentTarget)
            {
                // m_manSpine.LookAt(new Vector3(m_currentTarget.transform.position.x, m_manSpine.transform.position.y, m_currentTarget.transform.position.z));
                // m_manSpine.rotation = new Quaternion(m_manSpine.rotation.x, 0, m_manSpine.rotation.z, 0);
                // m_manSpine.forward = Vector3.Normalize(new Vector3(m_currentTarget.transform.position.x - m_manSpine.transform.position.x, 1, m_currentTarget.transform.position.x - m_manSpine.transform.position.x));
                var lookPos = m_currentTarget.transform.position - m_manSpine.transform.position;
                lookPos.y = m_manSpine.transform.position.y;
                var rotation = Quaternion.LookRotation(lookPos);
                rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
                // m_manSpine.transform.rotation = Quaternion.Slerp(m_manSpine.transform.rotation, rotation, Time.deltaTime);
                m_manSpine.transform.rotation = rotation;
            }
            // m_manSpine.forward = m_currentTarget.transform.position - m_manSpine.transform.position;

        }
    }
}
