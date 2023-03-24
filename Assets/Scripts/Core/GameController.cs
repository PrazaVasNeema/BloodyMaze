using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BloodyMaze.Controllers;
using TMPro;

namespace BloodyMaze
{

    public class GameController : MonoBehaviour
    {
        public static GameController instance { get; private set; }

        [SerializeField] private GameObject m_loader;
        [SerializeField] private bool m_shouldInitNewData;
        [SerializeField] private LocDataSO m_locData;
        public static LocDataSO locData => instance.m_locData;
        [SerializeField] private MusicGroupsSO m_musicGroups;
        public static MusicGroupsSO musicGroups => instance.m_musicGroups;
        [SerializeField] private TMP_Text m_TMP_Text;
        [SerializeField] private PlayerProfileSO m_playerProfile;
        public static PlayerProfileSO playerProfile => instance.m_playerProfile;
        [SerializeField] private LevelsInfoSO m_levelsInfo;
        public static LevelsInfoSO levelsInfo => instance.m_levelsInfo;

        private List<PlayerProfileData> m_allPlayerProfilesData = new();
        public static List<PlayerProfileData> allPlayerProfilesData => instance.m_allPlayerProfilesData;

        public static bool shouldStartNewGame;

        private static int m_choosenProfileIndex = 0;
        public static int choosenProfileIndex => m_choosenProfileIndex;

        private bool m_isReloaded;



        private LevelController m_levelController;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("instance not null");
                Destroy(gameObject);
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            m_loader.SetActive(false);
        }

        private void OnEnable()
        {
            GameEvents.OnEventFlagCheck += CheckEvent;
            GameEvents.OnSetNewRoomAgentStatus += SetRoomAgentStatus;
        }

        private void OnDisable()
        {
            GameEvents.OnEventFlagCheck -= CheckEvent;
            GameEvents.OnSetNewRoomAgentStatus -= SetRoomAgentStatus;
        }

        private void Start()
        {
            Debug.Log("START GAMECONTROLLER");
            //LoadPlayerProfile();
            LoadPlayerProfileGameplayData();
            LoadPlayerProfileOptionsData();
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                MusicManager.current.SetJam("MainMenu");
            }
            if (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "BattleSystem"
            || SceneManager.GetActiveScene().name == "BattleSystem_2")
            {
                MusicManager.current.SetJam("Gameplay");
                shouldStartNewGame = instance.m_shouldInitNewData;
            }

        }

        IEnumerator WaitForInitLevelCompleteCo()
        {
            bool DoOnce = true;
            while (DoOnce)
            {
                DoOnce = false;
                yield return new WaitForSecondsRealtime(2f);
            }
            GameEvents.OnInitLevelComplete?.Invoke();
        }

        private void OnApplicationQuit()
        {
        }

        private static void CheckEvent(string eventKey)
        {
            instance.m_playerProfile.playerProfileData.globalEventsData.Find((x) => x.eventKey == eventKey).flag = true;
        }
        private static void SetRoomAgentStatus(int roomID, int agentID)
        {
            Debug.Log("fghhgfjjghjghkjkhhjkhjk");
            playerProfile.SetRoomAgentStatus(roomID, agentID);
        }

        private static void InitLevel()
        {
            instance.m_levelController.Init();
            instance.StartCoroutine(instance.WaitForInitLevelCompleteCo());
        }

        private static void LoadPlayerProfileGameplayData()
        {
            var json = "";
            for (int i = 0; i < 3; i++)
            {
                json = PlayerPrefs.GetString($"PlayerProfile_{i}");
                instance.m_allPlayerProfilesData.Add(JsonUtility.FromJson<PlayerProfileData>(json));
            }
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "BattleSystem"
            || SceneManager.GetActiveScene().name == "BattleSystem_2")
            {
                if (!instance.m_shouldInitNewData)
                {
                    json = PlayerPrefs.GetString("PlayerProfile_0");
                    Debug.Log($">>> load {json}");
                }
                else
                {
                    json = "";
                }
                instance.m_playerProfile.LoadFromJsonGameplay(json, instance.m_shouldInitNewData);
                instance.m_levelController = FindObjectOfType<LevelController>();
                InitLevel();
            }
#endif
        }

        private void SetPlayerProfileSOData()
        {
            instance.m_playerProfile.LoadFromJsonGameplay(JsonUtility.ToJson(m_allPlayerProfilesData[m_choosenProfileIndex]), shouldStartNewGame);
        }

        private static void LoadPlayerProfileOptionsData()
        {
            var json = "";
            if (!instance.m_shouldInitNewData)
            {
                json = PlayerPrefs.GetString("PlayerProfile_0");
                Debug.Log($">>> load {json}");
            }
            instance.m_playerProfile.LoadFromJsonOptions(json);
        }

        public static void SaveData()
        {
            GameEvents.OnSaveData?.Invoke();
            var json = instance.m_playerProfile.ToJsonGameplay();
            Debug.Log($">>> save {json}");
            PlayerPrefs.SetString($"PlayerProfile_{m_choosenProfileIndex}", json);
        }

        public static void SaveDataWithoutOnSave()
        {
            var json = instance.m_playerProfile.ToJsonGameplay();
            Debug.Log($">>> save {json}");
            PlayerPrefs.SetString($"PlayerProfile_{m_choosenProfileIndex}", json);
        }

        public static void LoadScene(string sceneName, int choosenProfileIndex)
        {
            m_choosenProfileIndex = choosenProfileIndex;
            instance.StartCoroutine(instance.LoadSceneAsync(sceneName));
        }
        public static void LoadScene(string sceneName)
        {
            instance.StartCoroutine(instance.LoadSceneAsync(sceneName));
        }

        public static void LoadScene(string sceneName, bool isReloaded)
        {
            instance.m_isReloaded = isReloaded;
            instance.StartCoroutine(instance.LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            float timer = Time.unscaledTime;
            m_loader.SetActive(true);

            yield return SceneManager.LoadSceneAsync("Empty");

            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            if (m_isReloaded)
            {
                m_allPlayerProfilesData[m_choosenProfileIndex].roomsData = playerProfile.defaultData.roomsData;
                playerProfile.SetPlayerProfileSOData(m_allPlayerProfilesData[m_choosenProfileIndex]);
                SaveDataWithoutOnSave();
                m_isReloaded = false;
            }
            SetPlayerProfileSOData();
            var dif = Time.unscaledTime - timer;
            if (dif < 1)
            {
                yield return new WaitForSecondsRealtime(1 - dif);
            }
            yield return SceneManager.LoadSceneAsync(sceneName);
            switch (sceneName)
            {
                case "SampleScene":
                    m_levelController = FindObjectOfType<LevelController>();
                    InitLevel();
                    MusicManager.current.SetJam("Gameplay");
                    break;
                case "BattleSystem_2":
                    m_levelController = FindObjectOfType<LevelController>();
                    InitLevel();
                    MusicManager.current.SetJam("Gameplay");
                    break;
                case "MainMenu":

                    MusicManager.current.SetJam("MainMenu");
                    break;

            }
            m_loader.SetActive(false);
        }

        public static void SetLoaderText(string text)
        {
            instance.m_TMP_Text.text = text;
        }
    }

}