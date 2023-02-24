using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class CharacterComponent : MonoBehaviour
    {
        // [SerializeField] private CharacterData m_data;
        [HideInInspector] public HealthComponent healthComponent;
        [HideInInspector] public ManaComponent manaComponent;
        [HideInInspector] public MovementComponentCharacter movementComponentCharacter;
        [HideInInspector] public AbilitiesManager abilitiesManagerSlot1;
        [HideInInspector] public AbilitiesManager abilitiesManagerSlot2;
        [HideInInspector] public AmmunitionComponent ammunitionComponent;

        private void Awake()
        {
            TryGetComponent(out healthComponent);
            TryGetComponent(out manaComponent);
            TryGetComponent(out movementComponentCharacter);
            AbilitiesManager[] abilitiesManagers;
            abilitiesManagers = GetComponents<AbilitiesManager>();
            abilitiesManagerSlot1 = abilitiesManagers[0];
            abilitiesManagerSlot2 = abilitiesManagers[1];
            TryGetComponent(out ammunitionComponent);
        }
    }
}
