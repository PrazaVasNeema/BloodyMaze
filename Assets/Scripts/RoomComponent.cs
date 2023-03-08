using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class RoomComponent : MonoBehaviour
    {
        [SerializeField] private int m_roomID;
        public int roomID => m_roomID;
    }
}
