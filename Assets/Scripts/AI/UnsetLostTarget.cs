using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using BloodyMaze;

[System.Serializable]
public class UnsetLostTarget : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        blackboard.lostTarget = false;
        ActionStatesManager.ChangeEnemiesTriggeredCount(-1);
        ActionStatesManager.ChangeState();
        return State.Failure;
    }
}
