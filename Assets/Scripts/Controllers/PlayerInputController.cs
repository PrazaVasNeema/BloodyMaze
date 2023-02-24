using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BloodyMaze.Components;
using Cinemachine;

namespace BloodyMaze.Controllers
{

    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset m_inputAsset;
        [SerializeField] private CinemachineVirtualCamera m_virtualCamera;


        private CharacterComponent m_characterComponent;

        private InputAction m_moveAction;
        private InputAction m_attackAction;
        private InputAction m_swapWeaponAction;
        private InputAction m_useAbilityAction;
        private InputAction m_useAction;

        private void Awake()
        {
            m_moveAction = m_inputAsset.FindAction("Move");
            m_attackAction = m_inputAsset.FindAction("Fire");
            m_swapWeaponAction = m_inputAsset.FindAction("SwapWeapon");
            m_useAbilityAction = m_inputAsset.FindAction("UseAbility");
            m_useAction = m_inputAsset.FindAction("Use");
        }

        private void OnEnable()
        {
            m_inputAsset.FindActionMap("Player").Enable();
        }

        private void OnDisable()
        {
            m_inputAsset.FindActionMap("Player").Disable();
        }

        public void Init(CharacterComponent characterComponent)
        {
            m_characterComponent = characterComponent;
            // m_virtualCamera.Follow = character.transform;
        }

        private void Update()
        {
            if (m_characterComponent)
            {
                var move = m_moveAction.ReadValue<Vector2>();
                Vector3 offset = new(move.x, 0f, move.y);
                m_characterComponent.movementComponentCharacter.Move(offset);
                if (move.x != 0f || move.y != 0f)
                {
                    m_characterComponent.movementComponentCharacter.Look(offset);
                }
                if (m_attackAction.WasPressedThisFrame())
                {
                    m_characterComponent.abilitiesManager.UseAbility();
                }
                if (m_swapWeaponAction.WasPerformedThisFrame())
                {
                    m_characterComponent.abilitiesManager.NextAbility();
                }
            }
        }
    }

}
