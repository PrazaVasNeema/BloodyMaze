using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze
{
    [CreateAssetMenu(fileName = "PlayerProfileSO", menuName = "BloodyMaze/PlayerProfileSO")]
    public class PlayerProfileSO : ScriptableObject
    {
        [SerializeField] private PlayerProfileData m_default;

        public PlayerProfileData playerProfileData { private set; get; } = new();

        public CharacterSaveData GetCharacterSaveData()
        {
            return playerProfileData.characterSaveData;
        }

        public bool GetGlobalEventFlag(string eventKey)
        {
            return playerProfileData.globalEventsData.Find((x) => x.eventKey == eventKey).flag;
        }

        public RoomsData GetRoomsData(int roomID)
        {
            return playerProfileData.roomsData[roomID];
        }

        public void LoadFromJsonGameplay(string json, bool shouldInitNewData)
        {
            if (shouldInitNewData)
            {
                playerProfileData.characterSaveData = m_default.characterSaveData;
                playerProfileData.globalEventsData = m_default.globalEventsData;
                playerProfileData.roomsData = m_default.roomsData;
            }
            else
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var data = JsonUtility.FromJson<PlayerProfileData>(json);
                    if (data != null)
                    {
                        playerProfileData.characterSaveData = data.characterSaveData;
                        playerProfileData.globalEventsData = data.globalEventsData;
                        playerProfileData.roomsData = data.roomsData;
                    }
                }
            }
        }

        public string ToJsonGameplay()
        {
            PlayerProfileData currentData = new PlayerProfileData();
            currentData.characterSaveData = playerProfileData.characterSaveData;
            currentData.globalEventsData = playerProfileData.globalEventsData;
            currentData.roomsData = playerProfileData.roomsData;

            return JsonUtility.ToJson(currentData);
        }

        public void LoadFromJsonOptions(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                var data = JsonUtility.FromJson<PlayerProfileData>(json);
                if (data != null)
                {
                    playerProfileData.optionsData = data.optionsData;
                }
            }
            else
            {
                playerProfileData.optionsData = m_default.optionsData;
            }
        }
    }
}
