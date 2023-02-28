using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class AbilityComponentWreckingBall : IAbilityComponent
    {
        private float m_speed;
        private Vector3 m_targetPosition;
        public void UseAbility(float speed)
        {
            m_speed = speed;
        }
    }
}
