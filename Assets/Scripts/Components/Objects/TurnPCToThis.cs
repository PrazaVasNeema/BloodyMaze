using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class TurnPCToThis : MonoBehaviour
    {
        public void Activate()
        {
            Transform PCTransform = FindObjectOfType<CharacterController>().transform;
            PCTransform.LookAt(new Vector3(transform.position.x, PCTransform.position.y, transform.position.z));
        }
    }
}
