using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public interface IGameState
    {
        void Init(StateMachine stateMachine);
        void Enter();
        void Exit();
        IReadOnlyList<GameObject> GetViews();
    }
}