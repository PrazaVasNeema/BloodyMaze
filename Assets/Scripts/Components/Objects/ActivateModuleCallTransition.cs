using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class ActivateModuleCallTransition : ActivateModuleAbstract
    {
        [SerializeField] private RoomController m_prevRoom;
        [SerializeField] private RoomController m_nextRoom;
        [SerializeField] private bool m_shouldChangeRoom;
        [SerializeField] private Transform m_transitPoint;

        public override void ActivateModule()
        {
            GameTransitionSystem.current.TransitCharacter(m_transitPoint, m_prevRoom, m_nextRoom, m_shouldChangeRoom);
            var activateOnInteract = GetComponent<ActivateOnInteract>();
            if (activateOnInteract)
                activateOnInteract.interactComponent.OnInteract -= activateOnInteract.Activate;
            GameEvents.OnCallGotoFunction?.Invoke("none");
        }
    }
}
