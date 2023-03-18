using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class UnsetLostTarget : ActionNode
{
    protected override void OnStart()
    {
        blackboard.lostTarget = false;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
