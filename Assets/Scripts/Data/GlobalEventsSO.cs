using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(menuName = "BloodyMaze/GlobalEventsSO", fileName = "GlobalEventsSO")]
    public class GlobalEventsSO : ScriptableObject
    {
        public GlobalEventsData globalEventsData;
    }
}
