using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze
{
    [CreateAssetMenu(fileName = "PlayerProfileSO", menuName = "PlayerProfileSO")]
    public class PlayerProfileSO : ScriptableObject
    {
        public PlayerProfileData playerProfileData = new();

        public CharacterSaveData GetCharacterSaveData()
        {
            return playerProfileData.characterSaveData;
        }

        public RoomsData GetRoomsData(int roomID)
        {
            return playerProfileData.roomsData[roomID];
        }

        public void LoadFromJsonGameplay(string json, bool shouldInitNewData)
        {
            if (shouldInitNewData)
            {
                var dataDefault = Instantiate(GameController.instance.dataDefault);
                playerProfileData.characterSaveData = dataDefault.characterSaveData;
                playerProfileData.globalEventsData = dataDefault.globalEventsData;
                playerProfileData.roomsData = dataDefault.roomsData;
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
                var dataDefault = Instantiate(GameController.instance.dataDefault);
                playerProfileData.optionsData = dataDefault.optionsData;
            }
        }
    }
}
