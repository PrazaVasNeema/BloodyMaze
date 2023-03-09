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
        private GlobalEventsData m_globalEvents = new();
        public GlobalEventsData globalEvents => m_globalEvents;

        public CharacterSaveData GetCharacterSaveData()
        {
            return m_character;
        }

        public void LoadFromJson(string json, bool shouldInitNewData)
        {
            if (shouldInitNewData)
            {
                var characterDataDefault = Instantiate(GameController.instance.characterDataDefault);
                var globalEventsDefault = Instantiate(GameController.instance.globalEventsDefault);

                m_character = characterDataDefault.characterSaveData;
                m_globalEvents = globalEventsDefault.globalEventsData;
            }
            else
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var data = JsonUtility.FromJson<PlayerProfileData>(json);
                    if (data != null)
                    {
                        m_character = data.character;
                        m_globalEvents = data.globalEvents;
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
