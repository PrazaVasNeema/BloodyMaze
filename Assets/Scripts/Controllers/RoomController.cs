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
        [SerializeField] private InteractableComponentModulePickable[] m_roomItems;
        [SerializeField] private AgentIdentifier[] m_roomAgents;

        public void Init()
        {
            var globalEvents = GameController.instance.playerProfileSO.playerProfileData.globalEventsData;
            var temp = GameController.instance.playerProfileSO.playerProfileData.roomsData[m_roomID];
            List<AgentRoomStatus> agentsToSpawnIDs = new();
            if (temp != null)
                agentsToSpawnIDs = temp.agentsToSpawnIDs;
            for (int i = 0; i < m_roomItems.Length; i++)
            {
                var item = m_roomItems[i].gameObject.GetComponentInChildren<PickableItemComponent>();
                var globalEventsEvent = globalEvents.Find((x) => x.eventKey == m_roomItems[i].item.correspondingEventKey);
                if (globalEventsEvent != null && globalEventsEvent.flag)
                {
                    m_roomItems[i].gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < agentsToSpawnIDs.Count; i++)
            {
                var temp2 = agentsToSpawnIDs.Find((x) => x.agentID == m_roomAgents[i].agentID);
                if (temp2 != null && temp2.shouldntSpawn)
                {
                    m_roomAgents[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
