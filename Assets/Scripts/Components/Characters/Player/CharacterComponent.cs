using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class CharacterComponent : MonoBehaviour
    {
        [SerializeField] private CharacterSaveData m_characterSaveData;
        [HideInInspector] public HealthComponent healthComponent;
        [HideInInspector] public ManaComponent manaComponent;
        [HideInInspector] public MovementComponentCharacter movementComponentCharacter;
        [HideInInspector] public AbilitiesManager abilitiesManagerSlot1;
        [HideInInspector] public AbilitiesManager abilitiesManagerSlot2;
        [HideInInspector] public AbilitiesManager abilitiesManagerSlot3;
        [HideInInspector] public AmmunitionComponent ammunitionComponent;
        [HideInInspector] public InteractComponent interactComponent;
        [HideInInspector] public MedsComponent medsComponent;

        public void Init(CharacterSaveData characterSaveData)
        {
            m_characterSaveData = characterSaveData;

            if (TryGetComponent(out healthComponent))
            {
                healthComponent.Init(m_characterSaveData.currentHealth, m_characterSaveData.maxHealth);
            }

            if (TryGetComponent(out manaComponent))
            {
                manaComponent.Init(m_characterSaveData.currentMana, m_characterSaveData.maxMana, m_characterSaveData.manaRestoringRate);
            }

            if (TryGetComponent(out movementComponentCharacter))
            {
                movementComponentCharacter.Init(m_characterSaveData.moveSpeed);
            }

            if (TryGetComponent(out ammunitionComponent))
            {
                ammunitionComponent.Init(m_characterSaveData.holyAmmoType);
            }

            if (TryGetComponent(out medsComponent))
            {
                medsComponent.Init(m_characterSaveData.commonMeds);
            }

            TryGetComponent(out interactComponent);


            AbilitiesManager[] abilitiesManagers;
            abilitiesManagers = GetComponents<AbilitiesManager>();
            abilitiesManagerSlot1 = abilitiesManagers[0];
            abilitiesManagerSlot2 = abilitiesManagers[1];
            abilitiesManagerSlot3 = abilitiesManagers[2];
            // TryGetComponent(out ammunitionComponent);

            GameEvents.OnSaveData += SaveData;
        }

        private void OnDestroy()
        {
            GameEvents.OnSaveData -= SaveData;
        }

        private void SaveData()
        {
            m_characterSaveData.currentHealth = healthComponent.currentHealth;
            m_characterSaveData.holyAmmoType.currentAmmo = ammunitionComponent.m_ammoType["holy"].currentAmmo
            + ammunitionComponent.m_ammoType["holy"].currentRoundAmmo;
            m_characterSaveData.commonMeds.currentAmount = medsComponent.meds.currentAmount;
        }
    }
}
