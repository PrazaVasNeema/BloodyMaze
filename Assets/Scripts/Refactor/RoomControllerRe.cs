using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;

namespace BloodyMaze.Controllers
{
    public class RoomControllerRe : MonoBehaviour
    {
        [SerializeField] private string m_roomKey;
        public string roomKey => m_roomKey;
        [SerializeField] private ActivateModulePickUpItem[] m_roomItems;
        [SerializeField] private AgentIdentifier[] m_roomAgents;
        [SerializeField] private CheckIfObjectShouldSpawn[] m_roomActivaters;
        [SerializeField] private string m_eventFlagShouldBeCheckedToSpawnEnemies;
        [SerializeField] private Transform[] m_roomSpawnPoints;
        private List<GlobalEventsData> m_globalEventsData;
        private bool m_enabledFlag;

        private void Start()
        {
            InitLevelControllerController();
        }

        private static void InitLevelControllerController()
        {
#if UNITY_EDITOR
            if (GameController.instance == null)
            {
                var assets = UnityEditor.AssetDatabase.FindAssets("LevelController");
                foreach (var guid in assets)
                {
                    var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                    var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (prefab)
                    {
                        Instantiate(prefab);
                        break;
                    }
                }
            }
#endif
        }

        public void Init()
        {
            m_globalEventsData = GameController.playerProfile.playerProfileData.globalEventsData;

            var spawnPointNum = LevelControllerRe.current.currentSpawnPointIndex;

            LevelControllerRe.current.player.transform.position = m_roomSpawnPoints[spawnPointNum].position;
            LevelControllerRe.current.player.GetComponent<CharacterController>().enabled = true;

            // InitActivaters();
            // InitAgents();
            for (int i = 0; i < m_roomActivaters.Length; i++)
            {
                bool flag = true;
                foreach (string str in m_roomActivaters[i].eventsShouldBeChecked)
                {
                    var globalEventsEvent = m_globalEventsData.Find((x) => x.eventKey == str);
                    if (globalEventsEvent != null)
                    {
                        if (globalEventsEvent.flag == false)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                foreach (string str in m_roomActivaters[i].eventsShouldBeUnhecked)
                {
                    var globalEventsEvent = m_globalEventsData.Find((x) => x.eventKey == str);
                    if (globalEventsEvent != null)
                    {
                        if (globalEventsEvent.flag == true)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                ActivateModulePickUpItem activateModulePickUpItem = null;
                if (m_roomActivaters[i].transform.childCount > 0 && m_roomActivaters[i].transform.GetChild(0).TryGetComponent<ActivateModulePickUpItem>(out activateModulePickUpItem) && !flag)
                {
                    var globalEventsEvent = m_globalEventsData.Find((x) => x.eventKey == activateModulePickUpItem.eventKeyShouldBeUncheckedForItemToBeAdded);
                    if (globalEventsEvent != null && globalEventsEvent.flag == false)
                        GameInventory.current.AddItem(activateModulePickUpItem.item.item);
                }

                m_roomActivaters[i].gameObject.SetActive(flag);

                InitAgents();
            }



            // for (int i = 0; i < m_roomItems.Length; i++)
            // {
            //     var globalEventsEvent = m_globalEventsData.Find((x) => x.eventKey == m_roomItems[i].eventFlagCheck);
            //     if (globalEventsEvent != null && globalEventsEvent.flag)
            //     {
            //         GameInventory.current.AddItem(m_roomItems[i].item.item);
            //         m_roomItems[i].gameObject.SetActive(false);
            //     }
            // }
        }

        private void OnEnable()
        {
            if (m_enabledFlag)
            {
                InitActivaters();
                InitAgents();
                InitItems();
            }
        }

        public void InitActivaters()
        {
            for (int i = 0; i < m_roomActivaters.Length; i++)
            {
                bool flag = true;
                foreach (string str in m_roomActivaters[i].eventsShouldBeChecked)
                {
                    var globalEventsEvent = m_globalEventsData.Find((x) => x.eventKey == str);
                    if (globalEventsEvent != null)
                    {
                        if (globalEventsEvent.flag == false)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                foreach (string str in m_roomActivaters[i].eventsShouldBeUnhecked)
                {
                    var globalEventsEvent = m_globalEventsData.Find((x) => x.eventKey == str);
                    if (globalEventsEvent != null)
                    {
                        if (globalEventsEvent.flag == true)
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                m_roomActivaters[i].gameObject.SetActive(flag);

            }
        }

        public void InitItems()
        {

        }

        public void InitAgents()
        {
            Debug.Log("InitAgents");
            if (!string.IsNullOrEmpty(m_eventFlagShouldBeCheckedToSpawnEnemies) && !GameController.playerProfile.playerProfileData.globalEventsData.Find((x) =>
            x.eventKey == m_eventFlagShouldBeCheckedToSpawnEnemies).flag)
            {
                Debug.Log("InitAgents false");
                foreach (AgentIdentifier agent in m_roomAgents)
                    agent.gameObject.SetActive(false);
                return;
            }
            Debug.Log("InitAgents2");
            var temp = GameController.playerProfile.playerProfileData.roomsData.Find((x) => x.roomKey == m_roomKey);
            List<AgentRoomStatus> agentsToSpawnIDs = new();
            if (temp != null)
                agentsToSpawnIDs = temp.agentsToSpawnIDs;
            for (int i = 0; i < agentsToSpawnIDs.Count; i++)
            {
                var temp2 = agentsToSpawnIDs.Find((x) => x.agentID == m_roomAgents[i].agentID);
                if (temp2 != null && m_roomAgents[i] != null)
                {
                    m_roomAgents[i].gameObject.SetActive(temp2.shouldntSpawn ? false : true);
                }
                if (!temp2.shouldntSpawn && m_roomAgents[i] != null)
                    m_roomAgents[i].OnDead.AddListener(ChangeRoomAgentStatus);
            }
            Debug.Log("InitAgents3");

        }

        public void ShowAgents(List<int> agentsNumsToShow)
        {
            for (int i = 0; i < m_roomAgents.Length; i++)
            {
                if (agentsNumsToShow.Contains(i) && m_roomAgents[i] != null)
                {
                    m_roomAgents[i].gameObject.SetActive(true);
                }
            }
        }

        public void ShowAgentss(int[] agentsNumsToShow)
        {

        }

        public void HideAgents(List<int> agentsNumsToHide)
        {
            for (int i = 0; i < m_roomAgents.Length; i++)
            {
                if (!agentsNumsToHide.Contains(i))
                    continue;
                m_roomAgents[i].gameObject.SetActive(false);
            }
        }

        private void ChangeRoomAgentStatus(int agentID)
        {
            GameEvents.OnSetNewRoomAgentStatus?.Invoke(roomKey, agentID);
            m_roomAgents[agentID].GetComponent<AgentIdentifier>().OnDead.RemoveListener(ChangeRoomAgentStatus);
        }
    }
}
