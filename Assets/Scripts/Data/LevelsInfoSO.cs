using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(fileName = "LevelsInfoSO", menuName = "BloodyMaze/LevelsInfoSO")]
    public class LevelsInfoSO : ScriptableObject
    {
        [System.Serializable]
        public class Data
        {
            public string name;
            public Sprite icon;
            public string sceneName;
        }

        [SerializeField] private List<Data> m_levels;

        public IReadOnlyList<Data> GetLevels() => m_levels;

        public Data GetLevel(int index)
        {
            return m_levels[index];
        }

    }
}
