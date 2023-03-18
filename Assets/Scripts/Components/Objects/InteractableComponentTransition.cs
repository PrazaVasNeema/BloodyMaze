using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class InteractableComponentTransition : InteractableComponentModuleAbstract
    {
        [SerializeField] private RoomController m_prevRoom;
        [SerializeField] private RoomController m_nextRoom;
        [SerializeField] private bool m_shouldChangeRoom;
        [SerializeField] private Transform m_transitPoint;

        public override void ActivateModule()
        {
            GameTransitionSystem.current.TransitCharacter(m_transitPoint, m_prevRoom, m_nextRoom, m_shouldChangeRoom);
            var interactableComponent = GetComponent<InteractableComponent>();
            if (interactableComponent)
                interactableComponent.interactComponent.OnInteract -= interactableComponent.Activate;
            GameEvents.OnCallGotoFunction?.Invoke(4);
        }
    }
}
