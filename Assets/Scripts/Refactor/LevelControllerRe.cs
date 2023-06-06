using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;
using Cinemachine;
using BloodyMaze.UI;
using UnityEngine.SceneManagement;

namespace BloodyMaze.Controllers
{

    public class LevelControllerRe : MonoBehaviour
    {
        public static LevelControllerRe current { private set; get; }

        [SerializeField] private PlayerInputController m_playerInputController;
        [SerializeField] private UIPlayerHud m_playerHud;
        [SerializeField] private CharacterComponent m_playerPrefab;
        [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
        [SerializeField] private UIRootAnimationsController m_uiRootAnimationsController;
        public GameObject virtualCamera => m_virtualCamera.gameObject;
        private string m_startRoom;
        public CharacterComponent player;
        private int m_currentSpawnPointIndex;
        public int currentSpawnPointIndex => m_currentSpawnPointIndex;

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
            Debug.Log("LevelControllerInit");
            PlayerProfileSO playerProfileSO = GameController.instance.playerProfile;

            m_startRoom = GameController.instance.playerProfile.GetGlobalEventFlag("sat_at_desk") ?
                        "C1RSafe_zone" : "C1ROutsides";
            player = SpawnPlayer();
            player.GetComponent<CharacterController>().enabled = false;
            player.Init(playerProfileSO.GetCharacterSaveData());
            m_playerInputController.Init(player);
            m_playerHud.Init(player);
            // GameTransitionSystem.Init(player);
            m_virtualCamera.Follow = player.transform;
            DontDestroyOnLoad(gameObject);
            if (SceneManager.GetActiveScene().name.Contains("LevelPreLoader"))
                LoadRoomScene(m_startRoom, 0);
            else
            {
                var roomController = FindObjectOfType<RoomControllerRe>();
                m_currentSpawnPointIndex = 0;
                player.GetComponent<CharacterController>().enabled = false;
                roomController.Init();
                if (roomController.cameraBoundaries)
                    m_virtualCamera.GetComponent<CinemachineConfiner>().m_BoundingVolume = roomController.cameraBoundaries;
            }
        }

        private CharacterComponent SpawnPlayer()
        {
            return Instantiate(m_playerPrefab, gameObject.transform);
        }

        public void SetControlls(bool state)
        {
            m_playerInputController.enabled = state;
        }

        public void LoadRoomScene(string roomSceneName, int spawnPointNum)
        {
            m_currentSpawnPointIndex = spawnPointNum;
            StartCoroutine(LoadSceneAsyncRoom(roomSceneName));
        }

        private IEnumerator LoadSceneAsyncRoom(string sceneName)
        {
            player.GetComponent<CharacterController>().enabled = false;
            m_uiRootAnimationsController.FadeScreenExtra();
            yield return new WaitForSecondsRealtime(1f);
            yield return SceneManager.LoadSceneAsync("Empty");
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            // Chapter<Name>Room<Num>

            yield return SceneManager.LoadSceneAsync(sceneName);
            // GameTransitionSystem.ScreenFade();
            FindObjectOfType<RoomControllerRe>().Init();
            m_uiRootAnimationsController.UnfadeScreenExtra();
        }
    }

}
