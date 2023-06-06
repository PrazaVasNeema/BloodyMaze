using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class ActivateOnGameModeStateChange : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivate;
        [SerializeField] private string m_requiredModeName;
        [SerializeField] private List<string> m_keysOfFlagsThatShouldBeChecked;

        private void OnEnable()
        {
            GameEvents.OnStateChanged += Activate;
        }

        private void OnDisable()
        {
            GameEvents.OnStateChanged -= Activate;
        }

        private void Activate()
        {

            List<GlobalEventsData> eventsData = GameController.instance.playerProfile.playerProfileData.globalEventsData;
            bool requirementsSatisfied = true;
            foreach (string flagKey in m_keysOfFlagsThatShouldBeChecked)
            {
                if (eventsData.Find((x) => x.eventKey == flagKey) == null)
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
