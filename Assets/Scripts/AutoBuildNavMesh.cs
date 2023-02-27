using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BloodyMaze
{
    public class AutoBuildNavMesh : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface m_surface;

        private void Awake()
        {
            m_surface.BuildNavMesh();
        }
    }
}