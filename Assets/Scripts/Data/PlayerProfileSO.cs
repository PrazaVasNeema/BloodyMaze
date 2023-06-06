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

        [SerializeField] private PlayerProfileData[] m_defaultTest;
        public PlayerProfileData[] defaultTest => m_defaultTest;

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

        public void LoadFromJson(string json, bool shouldStartNewData)
        {
            if (shouldStartNewData)
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

        public string ToJson()
        {
            PlayerProfileData currentData = new PlayerProfileData();
            currentData.characterSaveData = playerProfileData.characterSaveData;
            currentData.globalEventsData = playerProfileData.globalEventsData;
            currentData.roomsData = playerProfileData.roomsData;
            return JsonUtility.ToJson(currentData);
        }
    }

    // Data classes ↓

    [System.Serializable]
    public class PlayerProfileData
    {
        public CharacterSaveData characterSaveData = new();
        public List<GlobalEventsData> globalEventsData = new();
        public List<RoomsData> roomsData = new();

        public PlayerProfileData Clone() => new PlayerProfileData
        {

            characterSaveData = this.characterSaveData,
            globalEventsData = this.globalEventsData,
            roomsData = this.roomsData
        };
    }

    [System.Serializable]
    public class CharacterSaveData
    {
        public float currentHealth;
        public float maxHealth;
        public float currentMana;
        public float maxMana;
        public float manaRestoringRate;
        public float moveSpeed;

        public AmmoType holyAmmoType;
        public MedsType commonMeds;
        public int lastLevelIndex;
        public int dayNum = 0;
    }

    [System.Serializable]
    public class GlobalEventsData
    {
        public string eventKey;
        public bool flag;
    }

    [System.Serializable]
    public class RoomsData
    {
        public string roomKey;
        public List<AgentRoomStatus> agentsToSpawnIDs = new();
        public List<ItemRoomStatus> itemsToSpawnIDs = new();
    }

    [System.Serializable]
    public class AgentRoomStatus
    {
        public int agentID;
        public bool shouldntSpawn;
    }

    [System.Serializable]
    public class ItemRoomStatus
    {
        public int itemID;
        public bool shouldntSpawn;
    }
}
