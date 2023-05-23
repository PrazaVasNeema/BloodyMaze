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

        private void Update()
        {
            if (Time.frameCount % 20 == 0)
            {
                if (!m_camera)
                    m_camera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
                else
                    transform.rotation = m_camera.transform.rotation;
            }
        }
    }
}
