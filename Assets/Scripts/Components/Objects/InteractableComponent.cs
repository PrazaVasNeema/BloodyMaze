using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(BoxCollider))]
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private string m_messageToShow = "Взаимодействовать";

        private InteractableComponentModuleAbstract m_interactableComponentModule;

        private void Awake()
        {
            if (TryGetComponent(out m_interactableComponentModule))
            {
                m_messageToShow = m_interactableComponentModule.messageToShow;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            InteractComponent interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (interactComponent && GameState.current.state == GameStates.EXPLORING)
            {
                interactComponent.OnInteract += Activate;
                GameEvents.OnUIGMessagesChangeState?.Invoke(m_messageToShow);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            InteractComponent interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (interactComponent && GameState.current.state == GameStates.EXPLORING)
            {
                interactComponent.OnInteract -= Activate;
                GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            }
        }

        private void Activate()
        {
            if (m_interactableComponentModule && GameState.current.state != GameStates.BATTLE)
            {
                GameState.current.ChangeState();
                GameEvents.OnSetInteractState?.Invoke();
                m_interactableComponentModule.Activate();
            }
            Debug.Log("Activate");
        }
    }
}
