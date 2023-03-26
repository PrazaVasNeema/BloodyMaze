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
        [SerializeField] private bool m_shouldWait;

        public override void ActivateModule()
        {
            if (m_shouldWait)
                GameTransitionSystem.current.TransitCharacter(m_transitPoint, m_prevRoom, m_nextRoom, m_shouldChangeRoom, true);
            else
                GameTransitionSystem.current.TransitCharacter(m_transitPoint, m_prevRoom, m_nextRoom, m_shouldChangeRoom);
            var activateOnInteract = GetComponent<ActivateOnInteract>();
            if (activateOnInteract)
                activateOnInteract.interactComponent.OnInteract -= activateOnInteract.Activate;
            GameEvents.OnCallGotoFunction?.Invoke("none");
        }

        public void InitRoomTransiter(ActivateModuleCallTransition activateModuleCallTransition)
        {
            m_prevRoom = transform.parent.GetComponent<RoomController>();
            m_nextRoom = activateModuleCallTransition.transform.parent.GetComponent<RoomController>();
            m_transitPoint = activateModuleCallTransition.transform.GetChild(0).transform;
        }
    }
}
