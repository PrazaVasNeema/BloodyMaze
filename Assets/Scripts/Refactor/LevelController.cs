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
        [SerializeField] private Transform[] m_spawnPoints;
        [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
        public GameObject virtualCamera => m_virtualCamera.gameObject;
        [SerializeField] private RoomController[] m_rooms;
        [SerializeField] private string m_activeRoom;
        [SerializeField] private List<ActivateModuleCallTransition> m_transiters;
        [SerializeField] private bool m_shouldHideRooms;

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
            PlayerProfileSO playerProfileSO = GameController.instance.playerProfile;

            m_activeRoom = GameController.instance.playerProfile.GetGlobalEventFlag("sat_at_desk") ?
                        "safe_zone" : "outsides";
            foreach (RoomController rc in m_rooms)
            {
                rc.Init();
                if (rc.roomKey == m_activeRoom)
                    rc.gameObject.SetActive(true);
                else
                if (m_shouldHideRooms)
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
            Transform spawnPoint = GameController.instance.playerProfile.GetGlobalEventFlag("sat_at_desk") ?
            m_spawnPoints[1] : m_spawnPoints[0];
            return Instantiate(m_playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        public void SetControlls(bool state)
        {
            m_playerInputController.enabled = state;
        }
    }

}
