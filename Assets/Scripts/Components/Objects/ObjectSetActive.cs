using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze
{
    public class ObjectSetActive : MonoBehaviour
    {
        [SerializeField] private GameObject m_objectToActivate;

        public void Activate()
        {
            m_objectToActivate.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
