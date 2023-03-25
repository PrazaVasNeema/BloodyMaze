using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class ActivateModuleCallEndGameState : ActivateModuleAbstract
    {
        public override void ActivateModule()
        {
            GameEvents.OnCallGotoFunction("end_game");
        }
    }
}
