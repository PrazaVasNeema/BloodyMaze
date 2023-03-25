using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    public class AIPatrolPositionsManager : MonoBehaviour
    {
        [SerializeField] bool m_shouldBeRandom;

        public Transform[] m_patrol_positions;
        private int m_currentPositionIndex = 0;

        public Vector2 ChooseNext()
        {
            Debug.Log("AIPatrolPositionsManager");
            if (m_patrol_positions.Length == 0)
            {
                return gameObject.transform.position;
            }
            Debug.Log("AIPatrolPositionsManager2");
            if (m_shouldBeRandom)
            {
                int index = Random.Range(0, m_patrol_positions.Length);
                return m_patrol_positions[index].position;
            }
            else
            {
                m_currentPositionIndex = (m_currentPositionIndex + 1) % m_patrol_positions.Length;
                return m_patrol_positions[m_currentPositionIndex].position;
            }
        }
    }

}