using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class InteractableComponentModuleChangeRoom : InteractableComponentModuleAbstract
    {
        [SerializeField] private float m_nextRoomID;

        public override void ActivateModule()
        {
            GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            GameTransitionSystem.current.TransitCharacter(transform);
        }
    }
}
