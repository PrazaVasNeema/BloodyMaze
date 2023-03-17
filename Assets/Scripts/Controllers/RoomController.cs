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

        public void Init()
        {
            var globalEvents = GameController.instance.playerProfileSO.playerProfileData.globalEventsData;
            for (int i = 0; i < m_roomItems.Length; i++)
            {
                var item = m_roomItems[i].gameObject.GetComponentInChildren<PickableItemComponent>();
                if (globalEvents[item.correspondingFlag].flag)
                {
                    m_roomItems[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
