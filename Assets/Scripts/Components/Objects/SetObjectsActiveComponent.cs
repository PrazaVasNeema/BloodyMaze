using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class SetObjectsActiveComponent : MonoBehaviour
    {
        [SerializeField] private bool m_shouldSetActive;
        [SerializeField] private bool m_shouldSetActiveFalseSelf;
        [SerializeField] private GameObject m_objectToActivate;

        public void Activate()
        {
            m_objectToActivate.SetActive(m_shouldSetActive);
            if (m_shouldSetActiveFalseSelf)
            {
                ActivateOnInteract activateOnInteract = GetComponent<ActivateOnInteract>();
                if (activateOnInteract)
                {
                    activateOnInteract.interactComponent.OnInteract -= activateOnInteract.Activate;
                    GameEvents.OnHideMessage?.Invoke();
                }
                gameObject.SetActive(false);
            }
        }
    }
}
