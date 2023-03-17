using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(menuName = "BloodyMaze/DataDefault", fileName = "DataDefault")]
    public class DataDefault : ScriptableObject
    {
        public CharacterSaveData characterSaveData;
        public List<GlobalEventsData> globalEventsData;
        public OptionsData optionsData;
        public List<RoomsData> roomsData;
    }
}
