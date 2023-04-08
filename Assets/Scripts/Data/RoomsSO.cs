using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{

    [CreateAssetMenu(fileName = "RoomsSO", menuName = "BloodyMaze/RoomsSO")]
    public class RoomsSO : ScriptableObject
    {
        public List<RoomData> roomDataList;
    }

    [System.Serializable]
    public class RoomData
    {
        public string RoomSceneName;
        public List<Transform> RoomTransitionPoints;
    }
}
