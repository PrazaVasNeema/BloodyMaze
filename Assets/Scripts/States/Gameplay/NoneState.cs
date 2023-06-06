using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public class NoneState : GameStateBehavior
    {
        private void OnEnable()
        {
            ActionStatesManager.SetState(ActionStates.INTERACTING);
        }

        private void OnDisable()
        {
            // if (ActionStatesManager.state == ActionStates.INTERACTING)
            //     ActionStatesManager.ChangeState();
        }
    }
}
