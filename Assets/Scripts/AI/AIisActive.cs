using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using BloodyMaze;

[System.Serializable]
public class AIisActive : DecoratorNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return ActionStatesManager.current.state == ActionStates.INTERACTING ? State.Running : child.Update();
    }
}
