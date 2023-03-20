using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class ShowOrHideRoomAgentsComponent : MonoBehaviour
    {
        [SerializeField] private List<int> m_agentsToAffect;
        [SerializeField] private bool m_shouldShow;
        [SerializeField] private RoomController m_roomController;

        public void ShowOrHide()
        {
            if (m_shouldShow)
                m_roomController.ShowAgents(m_agentsToAffect);
            else
                m_roomController.HideAgents(m_agentsToAffect);
        }
    }
}
