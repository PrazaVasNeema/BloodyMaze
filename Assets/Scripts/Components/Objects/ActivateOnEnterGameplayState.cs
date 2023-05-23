using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace BloodyMaze
{
    public class ActivateOnEnterGameplayState : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivate;
        [SerializeField] private string m_requiredModeName;
        [SerializeField] private List<string> m_keysOfFlagsThatShouldBeChecked;
        [SerializeField] private float m_delay;

        private void OnEnable()
        {
            GameEvents.OnEnterGameplayState += Activate;
        }

        private void OnDisable()
        {
            GameEvents.OnEnterGameplayState -= Activate;
        }

        private void Activate()
        {
            Invoke("WhatToActivate", 0.02f);

        }

        private void WhatToActivate()
        {
            List<GlobalEventsData> eventsData = GameController.playerProfile.playerProfileData.globalEventsData;
            bool requirementsSatisfied = true;
            foreach (string flagKey in m_keysOfFlagsThatShouldBeChecked)
            {
                if (eventsData.Find((x) => x.eventKey == flagKey).flag == false)
                {
                    requirementsSatisfied = false;
                    break;
                }
            }
            if (requirementsSatisfied)
                onActivate?.Invoke();
        }
    }
}
