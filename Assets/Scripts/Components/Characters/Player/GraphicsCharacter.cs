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
        private CharacterComponent m_characterComponent;
        private HealthComponent m_healthComponent;

        static int SpeedMoveId = Animator.StringToHash("MoveSpeed");
        static int m_isTurnedAroundID = Animator.StringToHash("IsTurnedAround");
        static int ShootRoundId = Animator.StringToHash("ShootRound");
        static int IsInBattleId = Animator.StringToHash("IsInBattle");
        static int ReloadId = Animator.StringToHash("Reload");
        static int ActivateBarrierId = Animator.StringToHash("ActivateBarrier");
        static int HolsterId = Animator.StringToHash("Holster");
        static int UnholsterId = Animator.StringToHash("Unholster");
        static int UseArmedId = Animator.StringToHash("UseArmed");
        static int UseUnarmedId = Animator.StringToHash("UseUnarmed");
        static int IsDeadId = Animator.StringToHash("IsDead");
        static int TakeDamageId = Animator.StringToHash("TakeDamage");





        private Vector3 m_lastPosition;
        private GameObject m_currentTarget;
        private bool m_isTurnedAround;
        private bool m_isInBattleState;

        private Quaternion m_manSpineErrorRoot;
        private void Awake()
        {
            m_manSpineErrorRoot = m_manSpine.rotation;
            m_animator = GetComponent<Animator>();
            m_abilitiesManager = GetComponentInParent<AbilitiesManager>();
            m_movementComponentCharacter = GetComponentInParent<MovementComponentCharacter>();
            m_characterComponent = GetComponentInParent<CharacterComponent>();
            m_healthComponent = GetComponentInParent<HealthComponent>();
        }

        private void OnEnable()
        {
            m_abilitiesManager.onUseAbility.AddListener(SetShootRoundTrigger);
            m_ability.OnTargetLockedChanged.AddListener(SetCurrentTarget);
            GameEvents.OnBattleActionStateIsSet += SetActionStateRelatedVars;
            m_healthComponent.onTakeDamage.AddListener(SetTakeDamageTrigger);
            m_healthComponent.onDead.AddListener(SetIsDeadBool);
        }

        private void OnDisable()
        {
            m_abilitiesManager.onUseAbility.RemoveListener(SetShootRoundTrigger);
            m_ability.OnTargetLockedChanged.RemoveListener(SetCurrentTarget);
            GameEvents.OnBattleActionStateIsSet -= SetActionStateRelatedVars;
            m_healthComponent.onTakeDamage.RemoveListener(SetTakeDamageTrigger);
            m_healthComponent.onDead.RemoveListener(SetIsDeadBool);
        }

        private void SetCurrentTarget(GameObject target)
        {
            m_currentTarget = target;
            if (m_currentTarget == null)
            {
                m_manSpine.LookAt(gameObject.transform.forward);
                transform.localRotation = Quaternion.Euler(0, 30, 0);
                m_isTurnedAround = false;
                m_animator.SetFloat(m_isTurnedAroundID, m_isTurnedAround ? 1 : 0);
            }
        }

        private void SetShootRoundTrigger()
        {
            m_animator.SetTrigger(ShootRoundId);
        }

        private void SetReloadTrigger()
        {
            m_animator.SetTrigger(ReloadId);
        }

        private void SetActivateBarrierTrigger()
        {
            m_animator.SetTrigger(ActivateBarrierId);
        }

        private void SetActionStateRelatedVars(bool isBattleState)
        {
            m_isInBattleState = isBattleState;
            m_animator.SetBool(IsInBattleId, m_isInBattleState);
            m_animator.SetTrigger(m_isInBattleState ? HolsterId : UnholsterId);
        }

        private void SetMatchingUseTrigger()
        {
            m_animator.SetTrigger(m_isInBattleState ? UseArmedId : UseUnarmedId);
        }

        private void SetIsDeadBool()
        {
            m_animator.SetBool(IsDeadId, true);
        }

        private void SetTakeDamageTrigger()
        {
            m_animator.SetTrigger(TakeDamageId);
        }

        private void LateUpdate()
        {
            // transform.LookAt(new Vector3(1, transform.position.y, 1));
            // transform.LookAt(m_currentTarget.transform);
            var velocity = Mathf.Clamp01(m_movementComponentCharacter.velocity / m_movementComponentCharacter.speed);
            m_animator.SetFloat(SpeedMoveId, velocity);
            // m_manSpine.LookAt(Vector3.Normalize(new Vector3(gameObject.transform.forward.x, gameObject.transform.forward.y, gameObject.transform.forward.z)));
            if (m_currentTarget)
            {
                var m_currentTargetPosition = m_currentTarget.transform.position;
                var dir = new Vector3(m_currentTargetPosition.x, m_manSpine.position.y, m_currentTargetPosition.z) - m_manSpine.position;
                var rotation = Quaternion.LookRotation(dir) * m_manSpineErrorRoot;
                m_manSpine.rotation = rotation;

                Vector3 PCForward = transform.forward, manSpineForward = m_manSpine.transform.forward;
                var dotProduct = PCForward.x * manSpineForward.x + PCForward.y * manSpineForward.y + PCForward.z * manSpineForward.z;
                var angle = Mathf.Acos(dotProduct / (PCForward.sqrMagnitude * manSpineForward.sqrMagnitude));
                // Debug.Log($"Angle: {angle}, {75f * Mathf.Deg2Rad}");
                if (angle > 1.57f)
                {
                    transform.localRotation = Quaternion.Euler(0, m_isTurnedAround ? 0 : 210, 0);
                    // transform.localScale = new Vector3(1, 1, -transform.localScale.z);
                    m_isTurnedAround = !m_isTurnedAround;
                }
                m_animator.SetFloat(m_isTurnedAroundID, m_isTurnedAround ? 1 : 0);

                // m_manSpine.LookAt(new Vector3(m_currentTarget.transform.position.x, m_manSpine.transform.position.y, m_currentTarget.transform.position.z));
                //     // m_manSpine.rotation = new Quaternion(m_manSpine.rotation.x, 0, m_manSpine.rotation.z, 0);
                //     // m_manSpine.forward = Vector3.Normalize(new Vector3(m_currentTarget.transform.position.x - m_manSpine.transform.position.x, 1, m_currentTarget.transform.position.x - m_manSpine.transform.position.x));
                //     var lookPos = m_currentTarget.transform.position - m_manSpine.transform.position;
                //     lookPos.y = m_manSpine.transform.position.y;
                //     var rotation = Quaternion.LookRotation(lookPos);
                //     rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
                //     // m_manSpine.transform.rotation = Quaternion.Slerp(m_manSpine.transform.rotation, rotation, Time.deltaTime);
                //     m_manSpine.transform.rotation = rotation;
            }
            // m_manSpine.forward = m_currentTarget.transform.position - m_manSpine.transform.position;

        }

        private class SubForCheckingAngle
        {

        }
    }
}
