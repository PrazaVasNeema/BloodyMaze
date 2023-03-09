using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(menuName = "BloodyMaze/CharacterData", fileName = "CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public CharacterSaveData characterSaveData;
    }
}
