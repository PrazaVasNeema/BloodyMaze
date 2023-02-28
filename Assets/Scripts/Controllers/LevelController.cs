using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;

namespace BloodyMaze.Controllers
{

    public class LevelController : MonoBehaviour
    {
        public static LevelController current { private set; get; }

        [SerializeField] private PlayerInputController m_playerInputController;
        [SerializeField] private UIPlayerHudControllerPStats m_playerHudControllerPStats;
        [SerializeField] private CharacterComponent m_playerPrefab;
        [SerializeField] private Transform m_spawnPoint;

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
            m_playerHudControllerPStats.Init(player);
        }

        private CharacterComponent SpawnPlayer()
        {
            return Instantiate(m_playerPrefab, m_spawnPoint.position, m_spawnPoint.rotation);
        }
    }

}
