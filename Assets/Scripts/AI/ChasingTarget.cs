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
            blackboard.targetPreviousSeenAt = context.agentComponent.m_previousSeenAtTransform;

        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {

            var targetPosition = blackboard.target == null ? blackboard.targetPreviousSeenAt : blackboard.target;
            bool isPreviousSeetAtTarget = blackboard.target == null ? true : false;
            if (targetPosition != null)
            {
                var agent = context.agent;
                if (isPreviousSeetAtTarget)
                    agent.stoppingDistance = .1f;
                else
                    agent.stoppingDistance = context.agentComponent.attackDistance;
                if (Time.frameCount % 3 == 0)
                {
                    agent.SetDestination(targetPosition.position);
                    if (blackboard.target != null)
                        blackboard.targetPreviousSeenAt.position = blackboard.target.position;
                }

                if (agent.pathPending)
                {
                    return State.Running;
                }

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    // blackboard.targetPreviousSeenAt = blackboard.target;
                    if (blackboard.target)
                        return State.Success;
                    else
                    {

                        Debug.Log("test");
                        blackboard.lostTarget = true;
                        blackboard.targetPreviousSeenAt = null;
                        return State.Failure;
                    }
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
