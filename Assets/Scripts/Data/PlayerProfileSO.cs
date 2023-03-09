using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze
{
    [CreateAssetMenu(fileName = "PlayerProfileSO", menuName = "PlayerProfileSO")]
    public class PlayerProfileSO : ScriptableObject
    {
        private CharacterSaveData m_character = new();

        public CharacterSaveData GetCharacterSaveData()
        {
            return m_character;
        }

        public void LoadFromJson(string json, bool shouldInitNewData)
        {
            if (shouldInitNewData)
            {
                m_character = GameController.instance.characterData.characterSaveData;
            }
            else
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var data = JsonUtility.FromJson<PlayerProfileData>(json);
                    if (data != null)
                    {
                        m_character = data.character;
                    }
                }
            }
        }

        public string ToJson()
        {
            PlayerProfileData data = new PlayerProfileData();
            data.character = m_character;

            return JsonUtility.ToJson(data);
        }
    }
}
