using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class ActivateModuleSetGameState : ActivateModuleAbstract
    {
        private enum GameStates { none, gameplay };
        [SerializeField] private GameStates m_gameStateToSet;
        public override void ActivateModule()
        {
            GameEvents.OnCallGotoFunction.Invoke(m_gameStateToSet.ToString());
        }
    }
}
