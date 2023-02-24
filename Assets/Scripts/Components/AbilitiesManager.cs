using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    public class AbilitiesManager : MonoBehaviour
    {
        [SerializeField] private Transform m_abilitiesSlot;
        private List<UseAbilityComponent> m_useAbilitityComponents = new();
        private UseAbilityComponent m_currentAbility;

        private void Awake()
        {
            if (m_abilitiesSlot == null)
            {
                m_abilitiesSlot = transform;
            }
            m_abilitiesSlot.GetComponentsInChildren(true, m_useAbilitityComponents);
            m_useAbilitityComponents.ForEach(x => x.SetActive(false));
        }

        public void Init(AbilityData[] abilitiesData, float baseDamage)
        {
            if (abilitiesData.Length == 0)
            {
                return;
            }

            m_useAbilitityComponents.ForEach(x => Destroy(x));
            m_useAbilitityComponents.Clear();

            foreach (AbilityData abilityData in abilitiesData)
            {
                var ability = Instantiate(abilityData.prefab, m_abilitiesSlot, false);
                // skill.Init(skillData.mana, skillData.cooldownTime, skillData.attackDistance, skillData.damage + baseDamage, skillData.flightSpeed, skillData.flightDistance);
                ability.SetActive(false);
                m_useAbilitityComponents.Add(ability);
            }

            SelectAbility(0);
        }

        private void Start()
        {
            SelectAbility(0);
        }


        public void NextAbility()
        {
            var index = m_useAbilitityComponents.IndexOf(m_currentAbility) + 1;
            if (index >= m_useAbilitityComponents.Count)
            {
                index = 0;
            }
            SelectAbility(index);
        }

        public void SelectAbility(int index)
        {
            if (index >= 0 && index < m_useAbilitityComponents.Count)
            {
                if (m_currentAbility != null)
                {
                    m_currentAbility.SetActive(false);
                }

                m_currentAbility = m_useAbilitityComponents[index];
                m_currentAbility.SetActive(true);
            }
        }

        public void UseAbility()
        {
            m_currentAbility.UseAbility();
        }

    }

}
