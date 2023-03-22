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
        }

        private void OnDisable()
        {
            GameEvents.OnEventFlagCheck -= CheckEvent;
        }

        private void Start()
        {
            //LoadPlayerProfile();
            LoadPlayerProfileGameplayData();
            LoadPlayerProfileOptionsData();

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
            MusicManager.current.SetJam("Gameplay");
        }

        private void OnApplicationQuit()
        {
        }

        private static void CheckEvent(string eventKey)
        {
            instance.m_playerProfile.playerProfileData.globalEventsData.Find((x) => x.eventKey == eventKey).flag = true;
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
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                if (!instance.m_shouldInitNewData)
                {
                    json = PlayerPrefs.GetString("PlayerProfile_0");
                    Debug.Log($">>> load {json}");
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
            instance.StartCoroutine(instance.SaveDataCo());
        }

        private IEnumerator SaveDataCo()
        {
            GameEvents.OnSaveData?.Invoke();
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(2f);
            }
            var json = m_playerProfile.ToJsonGameplay();
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

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            float timer = Time.unscaledTime;
            m_loader.SetActive(true);

            yield return SceneManager.LoadSceneAsync("Empty");

            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            SetPlayerProfileSOData();
            var dif = Time.unscaledTime - timer;
            if (dif < 1)
            {
                yield return new WaitForSecondsRealtime(1 - dif);
            }
            yield return SceneManager.LoadSceneAsync(sceneName);
            m_levelController = FindObjectOfType<LevelController>();
            m_loader.SetActive(false);
            InitLevel();
        }

        public static void SetLoaderText(string text)
        {
            instance.m_TMP_Text.text = text;
        }
    }

}