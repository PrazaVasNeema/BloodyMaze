using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Alerted : ActionNode
{
    protected override void OnStart()
    {
        blackboard.targetPreviousSeenAt = context.agentComponent.m_previousSeenAtTransform;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
