using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;
using Cinemachine;

namespace BloodyMaze.Controllers
{

    public class LevelController : MonoBehaviour
    {
        public static LevelController current { private set; get; }

        [SerializeField] private PlayerInputController m_playerInputController;
        [SerializeField] private UIPlayerHudControllerPStats m_playerHUDControllerPStats;
        [SerializeField] private CharacterComponent m_playerPrefab;
        [SerializeField] private Transform m_spawnPoint;
        [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
        [SerializeField] private GameTransitionSystem m_transitSystem;

        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        private void Start()
        {
            var player = SpawnPlayer();
            m_playerInputController.Init(player);
            m_playerHUDControllerPStats.Init(player);
            m_transitSystem.Init(player);
            m_virtualCamera.Follow = player.transform;
        }

        private CharacterComponent SpawnPlayer()
        {
            return Instantiate(m_playerPrefab, m_spawnPoint.position, m_spawnPoint.rotation);
        }
    }

}
