using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class GraphicsCharacter : MonoBehaviour
    {
        [SerializeField] private Transform m_manSpine;
        [SerializeField] private UseAbilityComponentModuleTargetedHitscan m_ability;
        [SerializeField] private GameObject m_revolverArmed;
        [SerializeField] private GameObject m_revolverUnarmed;
        [SerializeField] private GameObject m_revolverToDrop;
        [SerializeField] private Transform m_shootPoint;
        [SerializeField] private GameObject m_shootEffectPrefab;

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
        static int UseMedsArmedId = Animator.StringToHash("UseMedsArmed");
        static int UseMedsUnarmedId = Animator.StringToHash("UseMedsUnarmed");







        private Vector3 m_lastPosition;
        private GameObject m_currentTarget;
        private bool m_isTurnedAround;
        private bool m_isInBattleState;
        private bool m_isDead;

        private Quaternion m_manSpineErrorRoot;
        private void Awake()
        {
            m_manSpineErrorRoot = m_manSpine.rotation;
            m_animator = GetComponent<Animator>();
            m_abilitiesManager = GetComponentInParent<AbilitiesManager>();
            m_movementComponentCharacter = GetComponentInParent<MovementComponentCharacter>();
            m_characterComponent = GetComponentInParent<CharacterComponent>();
            m_healthComponent = GetComponentInParent<HealthComponent>();
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            m_revolverArmed.SetActive(false);
            m_revolverUnarmed.SetActive(true);
        }

        private void OnEnable()
        {
            m_abilitiesManager.onUseAbility.AddListener(SetShootRoundTrigger);
            m_ability.OnTargetLockedChanged.AddListener(SetCurrentTarget);
            GameEvents.OnBattleActionStateIsSet += SetActionStateRelatedVars;
            m_healthComponent.onTakeDamage.AddListener(SetTakeDamageTrigger);
            m_healthComponent.onDead.AddListener(SetIsDeadBool);
            transform.GetComponentInParent<InteractComponent>().OnInteract += SetMatchingUseTrigger;
        }

        private void OnDisable()
        {
            m_abilitiesManager.onUseAbility.RemoveListener(SetShootRoundTrigger);
            m_ability.OnTargetLockedChanged.RemoveListener(SetCurrentTarget);
            GameEvents.OnBattleActionStateIsSet -= SetActionStateRelatedVars;
            m_healthComponent.onTakeDamage.RemoveListener(SetTakeDamageTrigger);
            m_healthComponent.onDead.RemoveListener(SetIsDeadBool);
            transform.GetComponentInParent<InteractComponent>().OnInteract -= SetMatchingUseTrigger;
        }

        private void SetCurrentTarget(GameObject target)
        {
            m_currentTarget = target;
            if (m_currentTarget == null)
            {
                m_manSpine.LookAt(gameObject.transform.forward);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                m_isTurnedAround = false;
                m_animator.SetFloat(m_isTurnedAroundID, m_isTurnedAround ? 1 : 0);
            }
        }

        public void SetShootRoundTrigger()
        {
            Instantiate(m_shootEffectPrefab, m_shootPoint.position, m_shootPoint.rotation);
            m_animator.SetTrigger(ShootRoundId);
        }

        public void SetReloadTrigger()
        {
            m_animator.SetTrigger(ReloadId);
        }

        public void SetActivateBarrierTrigger()
        {
            m_animator.SetTrigger(ActivateBarrierId);
        }

        private void SetActionStateRelatedVars(bool isBattleState)
        {
            if (!m_isDead)
            {
                m_isInBattleState = isBattleState;
                m_animator.SetBool(IsInBattleId, m_isInBattleState);
                m_animator.SetTrigger(m_isInBattleState ? HolsterId : UnholsterId);
                transform.localRotation = Quaternion.Euler(0, isBattleState ? 0 : 0, 0);
                m_revolverArmed.SetActive(isBattleState);
                m_revolverUnarmed.SetActive(!isBattleState);
            }
        }

        private void SetMatchingUseTrigger()
        {
            m_animator.SetTrigger(m_isInBattleState ? UseArmedId : UseUnarmedId);
        }

        private void SetIsDeadBool()
        {
            m_isDead = true;
            m_revolverArmed.SetActive(false);
            m_revolverUnarmed.SetActive(false);
            m_revolverToDrop.SetActive(true);
            m_animator.SetBool(IsDeadId, true);
        }

        private void SetTakeDamageTrigger()
        {
            m_animator.SetTrigger(TakeDamageId);
        }

        public void SetMatchingUseMedsTrigger()
        {
            m_animator.SetTrigger(m_isInBattleState ? UseArmedId : UseUnarmedId);
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
                // var dir = new Vector3(m_currentTargetPosition.x, m_currentTargetPosition.y, m_currentTargetPosition.z) - m_manSpine.position;
                var rotation = Quaternion.LookRotation(dir) * m_manSpineErrorRoot;
                // if (velocity <= .5f)
                //     rotation = Quaternion.Euler(rotation.x, rotation.y + 30f, rotation.z);
                m_manSpine.rotation = rotation;

                Vector3 PCForward = transform.forward, manSpineForward = m_manSpine.transform.forward;
                var dotProduct = PCForward.x * manSpineForward.x + PCForward.y * manSpineForward.y + PCForward.z * manSpineForward.z;
                var angle = Mathf.Acos(dotProduct / (PCForward.sqrMagnitude * manSpineForward.sqrMagnitude));
                // Debug.Log($"Angle: {angle}, {75f * Mathf.Deg2Rad}");
                if (Mathf.Abs(angle) > 1.57f)
                {
                    transform.localRotation = Quaternion.Euler(0, m_isTurnedAround ? 0 : 180, 0);
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
