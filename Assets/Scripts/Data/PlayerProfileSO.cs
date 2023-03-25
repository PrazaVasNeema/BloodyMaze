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
        public PlayerProfileData defaultData => m_default;

        [SerializeField] public PlayerProfileData playerProfileData = new();

        public CharacterSaveData GetCharacterSaveData()
        {
            return playerProfileData.characterSaveData;
        }

        public void SetRoomAgentStatus(string roomID, int agentID)
        {
            playerProfileData.roomsData.Find((x) => x.roomKey == roomID).agentsToSpawnIDs.Find((x) => x.agentID == agentID).shouldntSpawn = true;
            Debug.Log(playerProfileData.roomsData.Find((x) => x.roomKey == roomID).agentsToSpawnIDs.Find((x) => x.agentID == agentID));
        }

        public void SetPlayerProfileSOData(PlayerProfileData playerProfileDataToSet)
        {
            playerProfileData = playerProfileDataToSet;
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
                PlayerProfileData currentData = new PlayerProfileData();
                currentData.characterSaveData = m_default.characterSaveData;
                currentData.globalEventsData = m_default.globalEventsData;
                currentData.roomsData = m_default.roomsData;
                var data = JsonUtility.FromJson<PlayerProfileData>(JsonUtility.ToJson(currentData));
                playerProfileData.characterSaveData = data.characterSaveData;
                playerProfileData.globalEventsData = data.globalEventsData;
                playerProfileData.roomsData = data.roomsData;
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
