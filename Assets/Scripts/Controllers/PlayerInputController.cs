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
        private InputAction m_interactAction;
        private InputAction m_reloadAction;

        private void Awake()
        {
            m_moveAction = m_inputAsset.FindAction("Move");
            m_attackAction = m_inputAsset.FindAction("Fire");
            m_swapWeaponAction = m_inputAsset.FindAction("SwapWeapon");
            m_useAbilityAction = m_inputAsset.FindAction("UseAbility");
            m_interactAction = m_inputAsset.FindAction("Interact");
            m_reloadAction = m_inputAsset.FindAction("Reload");
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
                    m_characterComponent.abilitiesManagerSlot1.UseAbility();
                }
                if (m_swapWeaponAction.WasPerformedThisFrame())
                {
                    m_characterComponent.abilitiesManagerSlot1.NextAbility();
                }
                if (m_useAbilityAction.WasPerformedThisFrame())
                {
                    m_characterComponent.abilitiesManagerSlot2.UseAbility();
                }
                if (m_reloadAction.WasPerformedThisFrame())
                {
                    switch (m_characterComponent.abilitiesManagerSlot1.currentAbilityIndex)
                    {
                        case 0:
                            m_characterComponent.ammunitionComponent.Reload("holy");
                            break;
                        case 1:
                            m_characterComponent.ammunitionComponent.Reload("silver");
                            break;
                    }
                }

                if (m_interactAction.WasPressedThisFrame())
                {
                    m_characterComponent.interactComponent.Interact();
                }
            }
        }
    }

}
