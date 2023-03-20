using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class ActivateModuleCheckEventFlag : ActivateModuleAbstract
    {
        public override void ActivateModule()
        {
            GameEvents.OnEventFlagCheck?.Invoke(m_eventFlagToCheck);
        }
    }
}
