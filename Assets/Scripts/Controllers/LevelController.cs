using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;
using Cinemachine;
using BloodyMaze.UI;

namespace BloodyMaze.Controllers
{

    public class LevelController : MonoBehaviour
    {
        public static LevelController current { private set; get; }

        [SerializeField] private PlayerInputController m_playerInputController;
        [SerializeField] private UIPlayerHud m_playerHud;
        [SerializeField] private CharacterComponent m_playerPrefab;
        [SerializeField] private Transform m_spawnPoint;
        [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
        [SerializeField] private RoomController[] m_rooms;
        [SerializeField] private int m_activeRoom;
        [SerializeField] private List<ActivateModuleCallTransition> m_transiters;

        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        public void Init()
        {
            PlayerProfileSO playerProfileSO = GameController.playerProfile;

            for (int i = 0; i < m_transiters.Count; i += 2)
            {
                m_transiters[i].InitRoomTransiter(m_transiters[i + 1]);
                m_transiters[i + 1].InitRoomTransiter(m_transiters[i]);
            }

            foreach (RoomController rc in m_rooms)
            {
                rc.Init();
                if (rc.roomID == m_activeRoom)
                    rc.gameObject.SetActive(true);
                else
                    rc.gameObject.SetActive(false);
            }

            var player = SpawnPlayer();
            player.Init(playerProfileSO.GetCharacterSaveData());
            m_playerInputController.Init(player);
            m_playerHud.Init(player);
            GameTransitionSystem.Init(player);
            m_virtualCamera.Follow = player.transform;
        }

        private CharacterComponent SpawnPlayer()
        {
            return Instantiate(m_playerPrefab, m_spawnPoint.position, m_spawnPoint.rotation);
        }

        public void SetControlls(bool state)
        {
            m_playerInputController.enabled = state;
        }
    }

}
