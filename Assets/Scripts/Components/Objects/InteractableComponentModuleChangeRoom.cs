using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class InteractableComponentModuleChangeRoom : InteractableComponentModuleAbstract
    {
        [SerializeField] private float m_nextRoomID;
        [SerializeField] private Transform m_transitPoint;

        public override void ActivateModule()
        {
            GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            GameTransitionSystem.current.TransitCharacter(m_transitPoint);
        }
    }
}
