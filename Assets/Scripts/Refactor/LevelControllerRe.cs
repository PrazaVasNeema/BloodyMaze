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
        public GameObject virtualCamera => m_virtualCamera.gameObject;
        private string m_startRoom;
        public CharacterComponent player;
        private int m_spawnPointNum;
        public int spawnPointNum => m_spawnPointNum;

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
            player = SpawnPlayer();
            player.GetComponent<CharacterController>().enabled = false;
            player.Init(playerProfileSO.GetCharacterSaveData());
            m_playerInputController.Init(player);
            m_playerHud.Init(player);
            // GameTransitionSystem.Init(player);
            m_virtualCamera.Follow = player.transform;
            DontDestroyOnLoad(gameObject);
            LoadRoomScene(m_startRoom, 0);
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
            m_spawnPointNum = spawnPointNum;
            StartCoroutine(LoadSceneAsyncRoom(roomSceneName));
        }

        private IEnumerator LoadSceneAsyncRoom(string sceneName)
        {
            player.GetComponent<CharacterController>().enabled = false;
            yield return SceneManager.LoadSceneAsync("Empty");
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            // Chapter<Name>Room<Num>

            yield return SceneManager.LoadSceneAsync(sceneName);
            // GameTransitionSystem.ScreenFade();
            FindObjectOfType<RoomControllerRe>().Init();
            // yield return new WaitForSecondsRealtime(2f);
        }
    }

}
