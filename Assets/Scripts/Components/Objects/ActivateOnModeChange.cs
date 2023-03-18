using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class ActivateOnModeChange : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivate;
        [SerializeField] private string m_requiredModeName;
        [SerializeField] private List<string> m_keysOfFlagsThatShouldBeChecked;

        private void OnEnable()
        {
            GameEvents.OnModeChange += Activate;
        }

        private void OnDisable()
        {
            GameEvents.OnModeChange -= Activate;
        }

        private void Activate(string modeName)
        {
            if (m_requiredModeName == modeName)
            {
                List<GlobalEventsData> eventsData = GameController.current.playerProfileSO.playerProfileData.globalEventsData;
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
}
