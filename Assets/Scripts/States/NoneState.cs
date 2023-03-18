using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public class NoneState : GameStateBehavior
    {
        private void OnEnable()
        {
            ActionStatesManager.current.ChangeState();
        }

        private void OnDisable()
        {
            if (ActionStatesManager.current.state == ActionStates.INTERACTING)
                ActionStatesManager.current.ChangeState();
        }
    }
}
