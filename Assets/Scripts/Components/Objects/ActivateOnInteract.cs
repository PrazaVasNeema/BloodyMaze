using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(BoxCollider))]
    public class ActivateOnInteract : MonoBehaviour
    {
        [SerializeField] private string m_messageTextKey = "interact";
        [SerializeField] private UnityEvent onActivate;
        [SerializeField] private LayerMask m_layerMask;

        private InteractComponent m_interactComponent;
        public InteractComponent interactComponent => m_interactComponent;
        private void OnEnable()
        {
            // GameEvents.OnEnterGameplayState += CheckIfTeleportedInside;
        }

        // private void OnEnable()
        // {
        //     GameEvents.OnHideMessage?.Invoke();
        //     BoxCollider collider = GetComponent<BoxCollider>();
        //     Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        //     Vector3 worldHalfExtents = collider.transform.TransformVector(collider.size * 0.5f);
        //     Collider[] colliders = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation, LayerMask.GetMask("Player"));
        //     if (colliders.Length > 0)
        //     {
        //         Debug.Log(colliders.Length);
        //         colliders[0].gameObject.GetComponentInParent<InteractComponent>().OnInteract += Activate;
        //         // m_interactComponent.OnInteract += Activate;
        //         GameEvents.OnShowMessage?.Invoke(m_messageTextKey);
        //     }
        // }

        private void OnDisable()
        {
            if (m_interactComponent != null)
                m_interactComponent.OnInteract -= Activate;
            // GameEvents.OnEnterGameplayState -= CheckIfTeleportedInside;
        }

        private void OnTriggerEnter(Collider other)
        {
            m_interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (m_interactComponent && ActionStatesManager.state == ActionStates.EXPLORING)
            {
                m_interactComponent.OnInteract += Activate;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            m_interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (m_interactComponent && ActionStatesManager.state == ActionStates.EXPLORING)
            {
                GameEvents.OnShowMessage?.Invoke(m_messageTextKey);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            m_interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (m_interactComponent)
            {
                m_interactComponent.OnInteract -= Activate;
                GameEvents.OnHideMessage?.Invoke();
            }
            m_interactComponent = null;
        }

        public void Activate()
        {
            onActivate?.Invoke();
        }

        public void CheckIfTeleportedInside()
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            Collider[] colliders = Physics.OverlapBox(collider.transform.TransformPoint(collider.center), GetComponent<BoxCollider>().transform.TransformVector(collider.size * 0.5f), collider.transform.rotation, LayerMask.GetMask("Player"));
            if (colliders.Length > 0)
            {
                m_interactComponent = colliders[0].gameObject.GetComponentInParent<InteractComponent>();
                if (m_interactComponent && ActionStatesManager.state == ActionStates.EXPLORING)
                {
                    m_interactComponent.OnInteract += Activate;
                }
            }

        }
    }
}
