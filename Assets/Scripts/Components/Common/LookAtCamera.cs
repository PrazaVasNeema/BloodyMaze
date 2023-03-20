using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;
using Cinemachine;

namespace BloodyMaze.Components
{
    public class LookAtCamera : MonoBehaviour
    {
        private GameObject m_camera;

        private void Awake()
        {
            m_camera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
        }

        private void Update()
        {
            if (Time.frameCount % 20 == 0)
            {
                transform.rotation = m_camera.transform.rotation;
            }
        }
    }
}
