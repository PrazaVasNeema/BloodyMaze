using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class CharacterComponent : MonoBehaviour
    {
        [SerializeField] private CharacterData m_data;
        [HideInInspector] public HealthComponent healthComponent;
        [HideInInspector] public ManaComponent manaComponent;
        [HideInInspector] public MovementComponentCharacter movementComponentCharacter;
        [HideInInspector] public AbilitiesManager abilitiesManagerSlot1;
        [HideInInspector] public AbilitiesManager abilitiesManagerSlot2;
        [HideInInspector] public AmmunitionComponent ammunitionComponent;
        [HideInInspector] public InteractComponent interactComponent;

        private void Awake()
        {

            if (TryGetComponent(out healthComponent))
            {
                healthComponent.Init(m_data.currentHealth, m_data.maxHealth);
            }

            if (TryGetComponent(out manaComponent))
            {
                manaComponent.Init(m_data.currentMana, m_data.maxMana, m_data.manaRestoringRate);
            }

            if (TryGetComponent(out movementComponentCharacter))
            {
                movementComponentCharacter.Init(m_data.moveSpeed);
            }

            if (TryGetComponent(out ammunitionComponent))
            {
                ammunitionComponent.Init(m_data.holyAmmoType, m_data.silverAmmoType);
            }

            TryGetComponent(out interactComponent);


            AbilitiesManager[] abilitiesManagers;
            abilitiesManagers = GetComponents<AbilitiesManager>();
            abilitiesManagerSlot1 = abilitiesManagers[0];
            abilitiesManagerSlot2 = abilitiesManagers[1];
            TryGetComponent(out ammunitionComponent);
        }
    }
}
