using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;
using Cinemachine;
using BloodyMaze.UI;

namespace BloodyMaze.Controllers
{

    public class LevelControllerRe : MonoBehaviour
    {
        public static LevelControllerRe current { private set; get; }

        [SerializeField] private PlayerInputController m_playerInputController;
        [SerializeField] private UIPlayerHud m_playerHud;
        [SerializeField] private CharacterComponent m_playerPrefab;
        [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
        public GameObject virtualCamera => m_virtualCamera.gameObject;
        [SerializeField] private string m_startRoom;

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

            m_startRoom = GameController.playerProfile.GetGlobalEventFlag("sat_at_desk") ?
                        "ChapterRavenWingRoomSafe_zone" : "ChapterRavenWingRoomOutsides";
            var player = SpawnPlayer();
            player.Init(playerProfileSO.GetCharacterSaveData());
            m_playerInputController.Init(player);
            m_playerHud.Init(player);
            GameTransitionSystem.Init(player);
            m_virtualCamera.Follow = player.transform;
            DontDestroyOnLoad(gameObject);
            GameController.LoadRoomScene(m_startRoom);
        }

        private CharacterComponent SpawnPlayer()
        {
            return Instantiate(m_playerPrefab, gameObject.transform);
        }

        public void SetControlls(bool state)
        {
            m_playerInputController.enabled = state;
        }
    }

}
