using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovementComponentAgent : MovementComponentAbstract
    {
        private NavMeshAgent m_agent;

        private void Awake()
        {
            m_agent = GetComponent<NavMeshAgent>();
        }

        public override void Move(Vector3 dir)
        {

            throw new System.NotImplementedException();
        }

        public override void MoveTo(Vector3 position)
        {
            m_agent.SetDestination(position);
        }

        public override void Init(float speed)
        {
            m_agent.speed = speed;
        }
    }
}
