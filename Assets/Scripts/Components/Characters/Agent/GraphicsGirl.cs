using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [RequireComponent(typeof(Animator))]
    public class GraphicsGirl : MonoBehaviour
    {
        private Animator m_animator;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Time.frameCount % 20 == 0)
                if (ActionStatesManager.state == ActionStates.INTERACTING)
                    m_animator.SetBool("IsTalking", true);
                else
                    m_animator.SetBool("IsTalking", false);
        }
    }
}
