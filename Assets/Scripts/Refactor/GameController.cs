using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BloodyMaze.Controllers;
using TMPro;
using UnityEngine.Audio;

// Сохранение данных, загрузка данных, загрузка сцены, контроль прогресса игры

namespace BloodyMaze
{

    public class GameController : MonoBehaviour
    {
        public static GameController instance { get; private set; }

        [SerializeField] private Animator m_UILoadingAnimator;
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


        [SerializeField] private List<PlayerProfileData> m_allPlayerProfilesData = new();
        public static List<PlayerProfileData> allPlayerProfilesData => instance.m_allPlayerProfilesData;

        public static bool shouldStartNewGame;

        private static int m_choosenProfileIndex = 0;
        public static int choosenProfileIndex => m_choosenProfileIndex;

        private bool m_gameShouldStart;

        [SerializeField] private string[] m_UILoaderTextsLocKeys = new string[4];
        [SerializeField] private TMP_Text m_UIToMainMenuText;
        [SerializeField] private TMP_Text m_UIToGameplayReText_1;
        [SerializeField] private TMP_Text m_UIToGameplayReText_2;
        [SerializeField] private TMP_Text m_UICommonLoadingCompleteTipText;
        [SerializeField] private GameOptionsSO m_gameOptions;
        public static GameOptionsSO gameOptions => instance.m_gameOptions;
        [SerializeField] private AudioMixer m_musicMixer;
        [SerializeField] private AudioMixer m_SFXMixer;
        private bool m_gameInitializeComplete = false;
        public static bool gameInitializeComplete => instance.m_gameInitializeComplete;


        public System.Action OnLoadingDataGameOptionsComplete;

        [SerializeField] private bool m_shouldUseTestDefault;

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
            LoadPlayerProfileGameplayData();
            LoadDataGameOptions();
        }

        public void InitInterfaceLocLoadingScreen()
        {
            m_UIToMainMenuText.text = locData.GetInterfaceText(m_UILoaderTextsLocKeys[0]);
            m_UIToGameplayReText_1.text = locData.GetInterfaceText(m_UILoaderTextsLocKeys[1]);
            m_UIToGameplayReText_2.text = locData.GetInterfaceText(m_UILoaderTextsLocKeys[2]);
            m_UICommonLoadingCompleteTipText.text = locData.GetInterfaceText(m_UILoaderTextsLocKeys[3]);
        }

        IEnumerator WaitForInitLevelCompleteCo()
        {
            yield return new WaitForSecondsRealtime(1f);
            m_gameInitializeComplete = true;
            GameEvents.OnInitLevelComplete?.Invoke();
        }

        private static void CheckEvent(string eventKey)
        {
            var globalEvent = instance.m_playerProfile.playerProfileData.globalEventsData.Find((x) => x.eventKey == eventKey);
            if (globalEvent != null)
                globalEvent.flag = true;
        }

        private static void SetRoomAgentStatus(string roomID, int agentID)
        {
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
                if (string.IsNullOrEmpty(json))
                    instance.m_allPlayerProfilesData.Add(instance.m_shouldUseTestDefault && i != 2 ? playerProfile.defaultTest[i] : null);
                else
                    instance.m_allPlayerProfilesData.Add(JsonUtility.FromJson<PlayerProfileData>(json));
            }
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name.Contains("Main"))
            {
                MusicManager.current.SetJam("MainMenu");
            }
            else
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
                instance.m_playerProfile.LoadFromJson(json, instance.m_shouldInitNewData);
                instance.m_levelController = FindObjectOfType<LevelController>();
                InitLevel();
                MusicManager.current.SetJam("Gameplay");
                shouldStartNewGame = instance.m_shouldInitNewData;
            }
#endif
        }

        private void SetPlayerProfileSOData()
        {
            instance.m_playerProfile.LoadFromJson(JsonUtility.ToJson(m_allPlayerProfilesData[m_choosenProfileIndex]), shouldStartNewGame);
        }

        public static void SaveData(bool shouldCallOnSave = true)
        {
            if (shouldCallOnSave)
                GameEvents.OnSaveData?.Invoke();
            var json = instance.m_playerProfile.ToJson();
            Debug.Log($">>> save {json}");
            PlayerPrefs.SetString($"PlayerProfile_{m_choosenProfileIndex}", json);
            instance.m_allPlayerProfilesData[m_choosenProfileIndex] = playerProfile.playerProfileData.Clone();
        }

        //GameOptionsZone START

        public void SetDataGameOptions(int language, float music, float sfx)
        {
            GameOptionsData newOptionsData = new();
            newOptionsData.language = language;
            Debug.Log($"set: {newOptionsData.language}");
            newOptionsData.volumeMusic = music;
            newOptionsData.volumeSFX = sfx;
            gameOptions.GameOptionsData = newOptionsData;
            Debug.Log($"set2: {gameOptions.GameOptionsData.language}");
            SaveDataGameOptions();
            LoadDataGameOptions();
        }

        public static void SaveDataGameOptions()
        {
            var json = instance.m_gameOptions.ToJsonGameOptions();
            Debug.Log($">>> save {json}");
            PlayerPrefs.SetString($"GameOptionsData", json);
        }

        private static void LoadDataGameOptions()
        {
            var json = "";
            json = PlayerPrefs.GetString("GameOptionsData");
            Debug.Log($">>> load (GameOptions) {json}");
            instance.m_gameOptions.LoadFromJsonGameOptions(json);
            instance.m_musicMixer.SetFloat("Master", instance.m_gameOptions.GameOptionsData.volumeMusic == 0f ? -80f : instance.m_gameOptions.GameOptionsData.volumeMusic * 30f - 25f);
            instance.m_SFXMixer.SetFloat("Master", instance.m_gameOptions.GameOptionsData.volumeSFX == 0f ? -80f : instance.m_gameOptions.GameOptionsData.volumeSFX * 30f - 25f);
            instance.OnLoadingDataGameOptionsComplete?.Invoke();
        }

        //GameOptionsZone END



        public static void LoadScene(string sceneName, int choosenProfileIndex = -1, bool isReloaded = false)
        {
            if (choosenProfileIndex != -1)
                m_choosenProfileIndex = choosenProfileIndex;
            if (isReloaded)
                instance.StartCoroutine(instance.LoadSceneAsyncReload(sceneName));
            else
                instance.StartCoroutine(instance.LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            m_gameShouldStart = false;
            float timer = Time.unscaledTime;
            m_loader.SetActive(true);
            m_UILoadingAnimator.SetBool("IsLoading", true);
            m_UILoadingAnimator.SetTrigger(sceneName == "MainMenu" ? "ToMainMenu" : "ToGameplay");
            yield return new WaitForSecondsRealtime(2f);
            yield return SceneManager.LoadSceneAsync("Empty");
            m_UICommonLoadingCompleteTipText.text = locData.GetInterfaceText(m_UILoaderTextsLocKeys[3]);
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            var timeToWait = 2f;
            // Chapter<Name>Room<Num>
            if (sceneName.Contains("Room"))
                SetPlayerProfileSOData();
            var dif = Time.unscaledTime - timer;
            if (dif < timeToWait)
            {
                yield return new WaitForSecondsRealtime(timeToWait - dif);
            }
            yield return SceneManager.LoadSceneAsync(sceneName);
            m_UILoadingAnimator.SetBool("IsLoading", false);
            if (sceneName == "MainMenu")
                MusicManager.current.SetJam("MainMenu");
            else
            {
                m_levelController = FindObjectOfType<LevelController>();
                InitLevel();
                MusicManager.current.SetJam("Gameplay");
                GameTransitionSystem.ScreenFade();
                while (!m_gameShouldStart)
                {
                    yield return new();
                }
            }
            m_loader.SetActive(false);
            if (sceneName != "MainMenu")
                GameTransitionSystem.ScreenUnfade();
            yield return new WaitForSecondsRealtime(2f);
        }

        // Scene reload case
        private IEnumerator LoadSceneAsyncReload(string sceneName)
        {
            m_gameShouldStart = false;
            float timer = Time.unscaledTime;
            m_loader.SetActive(true);
            m_UILoadingAnimator.SetBool("IsLoading", true);
            m_UILoadingAnimator.SetTrigger("ToGameplayRe");
            yield return new WaitForSecondsRealtime(2f);
            yield return SceneManager.LoadSceneAsync("Empty");

            m_UIToGameplayReText_2.text = $"{locData.GetInterfaceText(m_UILoaderTextsLocKeys[2])} {playerProfile.playerProfileData.characterSaveData.dayNum + 1}";
            m_UICommonLoadingCompleteTipText.text = locData.GetInterfaceText(m_UILoaderTextsLocKeys[4]);

            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            var timeToWait = 5f;
            m_allPlayerProfilesData[m_choosenProfileIndex].roomsData = playerProfile.defaultData.roomsData;
            playerProfile.SetPlayerProfileSOData(m_allPlayerProfilesData[m_choosenProfileIndex]);
            CheckEvent($"DayIsBehind_{playerProfile.playerProfileData.characterSaveData.dayNum}");
            playerProfile.playerProfileData.characterSaveData.dayNum++;
            SaveData(false);
            SetPlayerProfileSOData();
            var dif = Time.unscaledTime - timer;
            if (dif < timeToWait)
            {
                yield return new WaitForSecondsRealtime(timeToWait - dif);
            }
            yield return SceneManager.LoadSceneAsync(sceneName);
            m_UILoadingAnimator.SetBool("IsLoading", false);
            m_levelController = FindObjectOfType<LevelController>();
            InitLevel();
            GameTransitionSystem.ScreenFade();
            while (!m_gameShouldStart)
            {
                yield return new();
            }
            m_loader.SetActive(false);
            GameTransitionSystem.ScreenUnfade();
            yield return new WaitForSecondsRealtime(2f);
            // GameEvents.OnCallGotoFunction("gameplay");
        }

        public void SetGameShouldStart()
        {
            m_gameShouldStart = true;
        }




    }
}