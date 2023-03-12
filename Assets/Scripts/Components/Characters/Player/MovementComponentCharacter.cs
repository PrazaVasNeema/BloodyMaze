using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementComponentCharacter : MovementComponentAbstract
    {
        [SerializeField] private float m_speed = 5f;
        public float speed => m_speed;
        public float velocity => m_characterController.velocity.magnitude;

        private CharacterController m_characterController;

        private void Awake()
        {
            m_characterController = GetComponent<CharacterController>();
        }

        public override void Move(Vector3 dir)
        {
            m_characterController.SimpleMove(dir * m_speed);
        }

        public override void MoveTo(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public override void Init(float speed)
        {
            m_speed = speed;
        }
    }
}
