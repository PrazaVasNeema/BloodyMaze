using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public class NoneState : GameStateBehavior
    {
        private void OnEnable()
        {
            ActionStatesManager.ChangeState();
        }

        private void OnDisable()
        {
            // if (ActionStatesManager.state == ActionStates.INTERACTING)
            //     ActionStatesManager.ChangeState();
        }
    }
}
