using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{

    public class AbilitiesManager : MonoBehaviour
    {
        [SerializeField] private Transform m_abilitiesSlot;
        private List<UseAbilityComponent> m_useAbilitityComponents = new();
        private UseAbilityComponent m_currentAbility;
        private int m_currentAbilityIndex = 0;
        public int currentAbilityIndex => m_currentAbilityIndex;

        public System.Action<int> onAbilityChange;
        public UnityEvent onUseAbility;

        private void Awake()
        {
            if (m_abilitiesSlot == null)
            {
                m_abilitiesSlot = transform;
            }
            m_abilitiesSlot.GetComponentsInChildren(true, m_useAbilitityComponents);
            m_useAbilitityComponents.ForEach(x => x.SetActive(false));
        }

        private void Start()
        {
            SelectAbility();
        }


        public void NextAbility()
        {
            m_currentAbilityIndex = m_useAbilitityComponents.IndexOf(m_currentAbility) + 1;
            if (m_currentAbilityIndex >= m_useAbilitityComponents.Count)
            {
                m_currentAbilityIndex = 0;
            }
            SelectAbility();
        }

        public void SelectAbility()
        {
            if (m_currentAbilityIndex >= 0 && m_currentAbilityIndex < m_useAbilitityComponents.Count)
            {
                if (m_currentAbility != null)
                {
                    m_currentAbility.SetActive(false);
                }

                m_currentAbility = m_useAbilitityComponents[m_currentAbilityIndex];
                m_currentAbility.SetActive(true);
                onAbilityChange?.Invoke(m_currentAbilityIndex);

            }
        }

        public void UseAbility()
        {
            if (m_currentAbility.UseAbility())
            {
                onUseAbility?.Invoke();
            }
        }

    }

}
