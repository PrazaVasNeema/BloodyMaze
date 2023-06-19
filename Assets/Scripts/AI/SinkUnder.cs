using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using BloodyMaze;

namespace BloodyMaze.AI
{
    [System.Serializable]
    public class SinkUnder : ActionNode
    {
        public float m_sinkSpeed = 1f;
        public float m_sinkDepth = -2f;
        private Vector3 m_targetPosition;

        protected override void OnStart()
        {
            m_targetPosition = new Vector3(0, m_sinkDepth, 0);
            m_targetPosition = context.transform.position + m_targetPosition;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Mathf.Abs(context.transform.position.y - m_targetPosition.y) > 0.01f)
            {
                var curPosition = context.agentComponent.transform;
                curPosition.position = Vector3.MoveTowards(curPosition.position, m_targetPosition, m_sinkSpeed * Time.deltaTime);

                // curPosition.position = new Vector3(100, 100, 100);
                // curPosition.position = new Vector3(curPosition.position.x, curPosition.position.y + m_sinkSpeed, curPosition.position.z);

                return State.Running;
            }
            return State.Success;
        }
    }
}