using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class TurnObjectToAnotherObject : MonoBehaviour
    {
        [SerializeField] private Transform m_shouldTurnToThis;
        [SerializeField] private bool m_shouldTurnToPC;

        public void Activate()
        {
            transform.LookAt(m_shouldTurnToPC ? FindObjectOfType<CharacterController>().transform
            : m_shouldTurnToThis != null ? m_shouldTurnToThis : transform);
        }
    }
}
