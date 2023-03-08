using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BloodyMaze.Components;

namespace BloodyMaze.Controllers
{

    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset m_inputAsset;

        private CharacterComponent m_characterComponent;

        private InputAction m_moveAction;
        private InputAction m_attackAction;
        private InputAction m_swapWeaponAction;
        private InputAction m_useAbilityAction;
        private InputAction m_interactAction;
        private InputAction m_reloadAction;
        private InputAction m_escAction;

        public bool m_menusAreOpen;

        private void Awake()
        {
            m_moveAction = m_inputAsset.FindAction("Move");
            m_attackAction = m_inputAsset.FindAction("Fire");
            m_swapWeaponAction = m_inputAsset.FindAction("SwapWeapon");
            m_useAbilityAction = m_inputAsset.FindAction("UseAbility");
            m_interactAction = m_inputAsset.FindAction("Interact");
            m_reloadAction = m_inputAsset.FindAction("Reload");
            m_escAction = m_inputAsset.FindAction("Esc");
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
        }

        private void Update()
        {
            if (m_characterComponent)
            {
                if (!m_menusAreOpen)
                {
                    if (GameState.current.state != GameStates.INTERACTING)
                    {
                        var move = m_moveAction.ReadValue<Vector2>();
                        Vector3 offset = new(move.x, 0f, move.y);
                        if (offset.x != 0 || offset.y != 0 || offset.z != 0)
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
                    }
                    if (m_interactAction.WasPressedThisFrame() && GameState.current.state != GameStates.BATTLE)
                    {
                        m_characterComponent.interactComponent.Interact();
                    }
                }
                if (m_escAction.WasPressedThisFrame())
                {
                    LevelController.current.ChangeMenusState(m_menusAreOpen);
                }
            }
        }
    }

}
