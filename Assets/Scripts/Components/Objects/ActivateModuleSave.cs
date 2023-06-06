using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class ActivateModuleSave : ActivateModuleAbstract
    {
        public override void ActivateModule()
        {
            GameEvents.OnEventFlagCheck?.Invoke(m_eventFlagToCheck);
            GameController.instance.SaveData();
        }
    }
}
