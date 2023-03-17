using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(BoxCollider))]
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private string m_messageTextKey = "0";
        [SerializeField] private UnityEvent onActivate;

        private InteractComponent m_interactComponent;
        public InteractComponent interactComponent => m_interactComponent;

        private void OnTriggerEnter(Collider other)
        {
            m_interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (m_interactComponent && GameState.current.state == GameStates.EXPLORING)
            {
                m_interactComponent.OnInteract += Activate;
                GameEvents.OnShowMessage?.Invoke(m_messageTextKey);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            m_interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (m_interactComponent && GameState.current.state == GameStates.EXPLORING)
            {
                m_interactComponent.OnInteract -= Activate;
                GameEvents.OnHideMessage?.Invoke();
            }
        }

        public void Activate()
        {
            onActivate?.Invoke();
        }
    }
}
