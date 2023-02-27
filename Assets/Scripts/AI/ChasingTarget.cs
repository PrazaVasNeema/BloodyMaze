using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace BloodyMaze.AI
{
    [System.Serializable]
    public class ChasingTarget : ActionNode
    {
        protected override void OnStart()
        {
            context.agent.stoppingDistance = context.agentComponent.attackDistance;
            context.agent.SetDestination(blackboard.target.position);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            var target = blackboard.target;
            if (target)
            {
                var agent = context.agent;
                if (Time.frameCount % 3 == 0)
                {
                    agent.SetDestination(target.position);
                }

                if (agent.pathPending)
                {
                    return State.Running;
                }

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    return State.Success;
                }

                if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
                {
                    return State.Failure;
                }

                return State.Running;
            }

            return State.Failure;
        }
    }
}
