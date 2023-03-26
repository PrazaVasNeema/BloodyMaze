using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(fileName = "GameOptionsSO", menuName = "BloodyMaze/GameOptionsSO")]
    [System.Serializable]
    public class GameOptionsSO : ScriptableObject
    {
        [SerializeField] private GameOptionsData m_defaultGameOptionsData;
        public GameOptionsData defaultGameOptionsData => m_defaultGameOptionsData;

        [SerializeField] public GameOptionsData GameOptionsData = new();

        public void LoadFromJsonGameOptions(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                GameOptionsData currentData = new GameOptionsData();

                currentData = m_defaultGameOptionsData;
                var data = JsonUtility.FromJson<GameOptionsData>(JsonUtility.ToJson(currentData));
                GameOptionsData = data;
            }
            else
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var data = JsonUtility.FromJson<GameOptionsData>(json);
                    if (data != null)
                    {
                        GameOptionsData = data;
                    }
                }
            }
        }

        public string ToJsonGameOptions()
        {
            GameOptionsData currentData = new GameOptionsData();
            currentData = GameOptionsData;
            return JsonUtility.ToJson(currentData);
        }


    }

    [System.Serializable]
    public class GameOptionsData
    {
        public int language;
        public float volumeMusic;
        public float volumeSFX;
    }


}
