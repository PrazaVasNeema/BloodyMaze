using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public class EndGameState : GameStateBehavior
    {
        private void OnEnable()
        {
            if (GameController.gameInitializeComplete)
                ActionStatesManager.SetState(ActionStates.INTERACTING);
        }
    }
}
