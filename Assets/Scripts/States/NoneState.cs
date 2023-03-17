using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public class NoneState : GameStateBehavior
    {
        private void OnEnable()
        {
            GameState.current.ChangeState();
        }

        private void OnDisable()
        {
            if (GameState.current.state == GameStates.INTERACTING)
                GameState.current.ChangeState();
        }
    }
}
