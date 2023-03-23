using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;

namespace BloodyMaze.Controllers
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private int m_roomID;
        public int roomID => m_roomID;
        [SerializeField] private ActivateModulePickUpItem[] m_roomItems;
        [SerializeField] private AgentIdentifier[] m_roomAgents;
        [SerializeField] private CheckIfObjectShouldSpawn[] m_roomActivaters;
        private List<GlobalEventsData> m_globalEventsData;
        private bool m_enabledFlag;


        public void Init()
        {
            GameEvents.OnInitLevelComplete += CheckEnableFlag;
            m_globalEventsData = GameController.playerProfile.playerProfileData.globalEventsData;

            // InitActivaters();
            // InitAgents();
            for (int i = 0; i < m_roomActivaters.Length; i++)
            {
                bool flag = true;
                Debug.Log(roomID);
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


            for (int i = 0; i < m_roomItems.Length; i++)
            {
                var globalEventsEvent = m_globalEventsData.Find((x) => x.eventKey == m_roomItems[i].eventFlagCheck);
                if (globalEventsEvent != null && globalEventsEvent.flag)
                {
                    GameInventory.current.AddItem(m_roomItems[i].item.item);
                    m_roomItems[i].gameObject.SetActive(false);
                }
            }
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

        private void CheckEnableFlag()
        {
            m_enabledFlag = true;
            GameEvents.OnInitLevelComplete -= CheckEnableFlag;
        }

        public void InitItems()
        {

        }

        public void InitAgents()
        {
            var temp = GameController.playerProfile.playerProfileData.roomsData[m_roomID];
            List<AgentRoomStatus> agentsToSpawnIDs = new();
            if (temp != null)
                agentsToSpawnIDs = temp.agentsToSpawnIDs;
            for (int i = 0; i < agentsToSpawnIDs.Count; i++)
            {
                var temp2 = agentsToSpawnIDs.Find((x) => x.agentID == m_roomAgents[i].agentID);
                if (temp2 != null && temp2.shouldntSpawn)
                {
                    m_roomAgents[i].gameObject.SetActive(false);
                }
            }
        }

        public void ShowAgents(List<int> agentsNumsToShow)
        {
            for (int i = 0; i < m_roomAgents.Length; i++)
            {
                if (agentsNumsToShow.Contains(i) && m_roomAgents[i] != null)
                    m_roomAgents[i].gameObject.SetActive(true);
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
    }
}
