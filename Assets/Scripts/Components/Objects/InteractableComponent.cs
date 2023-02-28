using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(BoxCollider))]
    public class InteractableComponent : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            InteractComponent interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (interactComponent)
            {
                interactComponent.OnInteract += Activate;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            InteractComponent interactComponent = other.gameObject.GetComponentInParent<InteractComponent>();
            if (interactComponent)
            {
                interactComponent.OnInteract -= Activate;
            }
        }

        private void Activate()
        {
            Debug.Log("Activate");
        }
    }
}
