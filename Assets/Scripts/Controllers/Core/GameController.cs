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
        [SerializeField] private TMP_Text m_reloadDayTextField;
        [SerializeField] private bool m_shouldInitNewData;
        [SerializeField] private LocDataSO m_locData;
        public LocDataSO locData => m_locData;
        [SerializeField] private MusicGroupsSO m_musicGroups;
        public MusicGroupsSO musicGroups => m_musicGroups;
        [SerializeField] private TMP_Text m_TMP_Text;
        [SerializeField] private PlayerProfileSO m_playerProfile;
        public PlayerProfileSO playerProfile => m_playerProfile;
        [SerializeField] private LevelsInfoSO m_levelsInfo;
        public LevelsInfoSO levelsInfo => m_levelsInfo;
        [SerializeField] private List<PlayerProfileData> m_allPlayerProfilesData = new();
        public List<PlayerProfileData> allPlayerProfilesData => m_allPlayerProfilesData;
        [SerializeField] private bool m_shouldUseTestDefault;
        [SerializeField] private GameOptionsSO m_gameOptions;
        public GameOptionsSO gameOptions => m_gameOptions;
        [SerializeField] private AudioMixer m_musicMixer;
        [SerializeField] private AudioMixer m_SFXMixer;

        public System.Action OnLoadingDataGameOptionsComplete;
        public bool shouldStartNewGame;

        private bool m_gameInitializeComplete = false;
        public bool gameInitializeComplete => m_gameInitializeComplete;
        private LevelControllerRe m_levelController;
        private int m_choosenProfileIndex = 0;
        public int choosenProfileIndex => m_choosenProfileIndex;
        private bool m_gameShouldStart;
        public bool gameShouldStart => m_gameShouldStart;

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

        private void CheckEvent(string eventKey)
        {
            var globalEvent = m_playerProfile.playerProfileData.globalEventsData.Find((x) => x.eventKey == eventKey);
            if (globalEvent != null)
                globalEvent.flag = true;
        }

        private void SetRoomAgentStatus(string roomID, int agentID)
        {
            playerProfile.SetRoomAgentStatus(roomID, agentID);
        }

        private void LoadPlayerProfileGameplayData()
        {
            var json = "";
            for (int i = 0; i < 3; i++)
            {
                json = PlayerPrefs.GetString($"PlayerProfile_{i}");
                if (string.IsNullOrEmpty(json))
                    m_allPlayerProfilesData.Add(m_shouldUseTestDefault && i != 2 ? playerProfile.defaultTest[i] : null);
                else
                    m_allPlayerProfilesData.Add(JsonUtility.FromJson<PlayerProfileData>(json));
            }
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name.Contains("MainMenu") || SceneManager.GetActiveScene().name.Contains("GameLoader"))
            {
                MusicManager.current.SetJam("MainMenu");
            }
            else
            {
                if (!m_shouldInitNewData)
                {
                    json = PlayerPrefs.GetString("PlayerProfile_0");
                    Debug.Log($">>> load {json}");
                }
                else
                {
                    json = "";
                }
                m_playerProfile.LoadFromJson(json, m_shouldInitNewData);
                m_levelController = FindObjectOfType<LevelControllerRe>();
                m_levelController.Init();
                MusicManager.current.SetJam("Gameplay");
                shouldStartNewGame = m_shouldInitNewData;
                m_gameShouldStart = true;
            }
#endif
        }

        private void SetPlayerProfileSOData()
        {
            m_playerProfile.LoadFromJson(JsonUtility.ToJson(m_allPlayerProfilesData[m_choosenProfileIndex]), shouldStartNewGame);
        }

        public void SaveData(bool shouldCallOnSave = true)
        {
            if (shouldCallOnSave)
                GameEvents.OnSaveData?.Invoke();
            var json = m_playerProfile.ToJson();
            Debug.Log($">>> save {json}");
            PlayerPrefs.SetString($"PlayerProfile_{m_choosenProfileIndex}", json);
            m_allPlayerProfilesData[m_choosenProfileIndex] = playerProfile.playerProfileData.Clone();
        }

        //GameOptionsZone START

        public void SetDataGameOptions(int language, float music, float sfx, int fpsLock)
        {
            var prevLanguageID = gameOptions.GameOptionsData.language;
            GameOptionsData newOptionsData = new();
            newOptionsData.language = language;
            Debug.Log($"set: {newOptionsData.language}");
            newOptionsData.volumeMusic = music;
            newOptionsData.volumeSFX = sfx;
            newOptionsData.fpsLockValue = fpsLock;
            gameOptions.GameOptionsData = newOptionsData;
            Debug.Log($"set2: {gameOptions.GameOptionsData.language}");
            SaveDataGameOptions();
            LoadDataGameOptions();
            if (prevLanguageID != language)
                LoadScene("MainMenu");
        }

        public void SaveDataGameOptions()
        {
            var json = m_gameOptions.ToJsonGameOptions();
            Debug.Log($">>> save {json}");
            PlayerPrefs.SetString($"GameOptionsData", json);
        }

        private void LoadDataGameOptions()
        {
            var json = "";
            json = PlayerPrefs.GetString("GameOptionsData");
            Debug.Log($">>> load (GameOptions) {json}");
            m_gameOptions.LoadFromJsonGameOptions(json);
            m_musicMixer.SetFloat("Master", m_gameOptions.GameOptionsData.volumeMusic == 0f ? -80f : m_gameOptions.GameOptionsData.volumeMusic * 30f - 25f);
            m_SFXMixer.SetFloat("Master", m_gameOptions.GameOptionsData.volumeSFX == 0f ? -80f : m_gameOptions.GameOptionsData.volumeSFX * 30f - 25f);
            Application.targetFrameRate = m_gameOptions.GameOptionsData.fpsLockValue;
            OnLoadingDataGameOptionsComplete?.Invoke();
        }

        //GameOptionsZone END



        public void LoadScene(string sceneName, int choosenProfileIndex = -1, bool isReloaded = false)
        {
            if (choosenProfileIndex != -1)
                m_choosenProfileIndex = choosenProfileIndex;
            if (isReloaded)
                StartCoroutine(LoadSceneAsyncReload(sceneName));
            else
                StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            m_gameShouldStart = false;
            float timer = Time.unscaledTime;
            m_loader.SetActive(true);
            m_UILoadingAnimator.SetBool("IsLoading", true);
            m_UILoadingAnimator.SetTrigger(sceneName == "MainMenu" ? "ToMainMenu" : "ToGameplay");
            if (SceneManager.GetActiveScene().name.Contains("C1"))
                SceneManager.MoveGameObjectToScene(FindAnyObjectByType<LevelControllerRe>().gameObject, SceneManager.GetActiveScene());
            yield return new WaitForSecondsRealtime(2f);
            yield return SceneManager.LoadSceneAsync("Empty");
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            var timeToWait = 2f;
            // Chapter<Name>Room<Num>
            var dif = Time.unscaledTime - timer;
            if (dif < timeToWait)
            {
                yield return new WaitForSecondsRealtime(timeToWait - dif);
            }
            yield return SceneManager.LoadSceneAsync(sceneName);
            if (sceneName == "MainMenu")
            {
                MusicManager.current.SetJam("MainMenu");
                m_UILoadingAnimator.SetBool("IsLoading", false);
            }
            else
            {
                SetPlayerProfileSOData();
                m_levelController = FindObjectOfType<LevelControllerRe>();
                m_levelController.Init();
                MusicManager.current.SetJam("Gameplay");
                // GameTransitionSystem.ScreenFade();
                m_UILoadingAnimator.SetBool("IsLoading", false);
                while (!m_gameShouldStart)
                {
                    yield return new();
                }
                FindAnyObjectByType<UIRootAnimationsController>().UnfadeScreenExtra();
            }
            m_loader.SetActive(false);

            // if (sceneName != "MainMenu")
            //     GameTransitionSystem.ScreenUnfade();
            yield return new WaitForSecondsRealtime(2f);
        }

        // Scene reload case
        private IEnumerator LoadSceneAsyncReload(string sceneName)
        {
            SceneManager.MoveGameObjectToScene(FindAnyObjectByType<LevelControllerRe>().gameObject, SceneManager.GetActiveScene());
            m_gameShouldStart = false;
            float timer = Time.unscaledTime;
            m_loader.SetActive(true);
            m_UILoadingAnimator.SetBool("IsLoading", true);
            m_UILoadingAnimator.SetTrigger("ToGameplayRe");
            yield return new WaitForSecondsRealtime(2f);
            yield return SceneManager.LoadSceneAsync("Empty");

            m_reloadDayTextField.text = $"{locData.GetInterfaceText("UILoc_ui_loading_to_gameplay_re_2")} {playerProfile.playerProfileData.characterSaveData.dayNum + 2}";

            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            var timeToWait = 5f;
            m_allPlayerProfilesData[m_choosenProfileIndex].roomsData = playerProfile.defaultData.roomsData;
            playerProfile.SetPlayerProfileSOData(m_allPlayerProfilesData[m_choosenProfileIndex]);
            CheckEvent($"DayIsBehind_{playerProfile.playerProfileData.characterSaveData.dayNum}");
            playerProfile.playerProfileData.characterSaveData.dayNum++;
            SaveData(false);
            var dif = Time.unscaledTime - timer;
            if (dif < timeToWait)
            {
                yield return new WaitForSecondsRealtime(timeToWait - dif);
            }
            yield return SceneManager.LoadSceneAsync(sceneName);
            SetPlayerProfileSOData();
            m_UILoadingAnimator.SetBool("IsLoading", false);
            m_levelController = FindObjectOfType<LevelControllerRe>();
            m_levelController.Init();
            var uiRootAnimationsController = FindObjectOfType<UIRootAnimationsController>();
            uiRootAnimationsController.FadeScreen();
            while (!m_gameShouldStart)
            {
                yield return new();
            }
            m_loader.SetActive(false);
            uiRootAnimationsController.UnfadeScreenExtra();

        }

        public void SetGameShouldStart()
        {
            m_gameShouldStart = true;
        }
    }
}