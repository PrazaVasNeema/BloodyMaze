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
        public LocDataSO locData => m_locData;
        [SerializeField] private TMP_Text m_TMP_Text;

        public PlayerProfileSO playerProfileSO;
        public DataDefault dataDefault;

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
            GameEvents.OnFlagCheck += CheckEvent;
        }

        private void OnDisable()
        {
            GameEvents.OnFlagCheck -= CheckEvent;
        }

        private void Start()
        {
            //LoadPlayerProfile();
            LoadPlayerProfileGameplayData();
            m_levelController = FindObjectOfType<LevelController>();
            InitLevel();
        }

        private void OnApplicationQuit()
        {
        }

        private void CheckEvent(string eventKey)
        {
            playerProfileSO.playerProfileData.globalEventsData.Find((x) => x.eventKey == eventKey).flag = true;
        }

        private void InitLevel()
        {
            m_levelController.Init();
        }

        private void LoadPlayerProfileGameplayData()
        {
            var json = "";
            if (!m_shouldInitNewData)
            {
                json = PlayerPrefs.GetString("PlayerProfile");
                Debug.Log($">>> load {json}");
            }
            playerProfileSO.LoadFromJsonGameplay(json, m_shouldInitNewData);
            // playerProfile.audioOptions.fxVolume;
            // playerProfile.audioOptions.musicVolume;
        }

        private void LoadPlayerProfileOptionsData()
        {
            var json = "";
            if (!m_shouldInitNewData)
            {
                json = PlayerPrefs.GetString("PlayerProfile");
                Debug.Log($">>> load {json}");
            }
            playerProfileSO.LoadFromJsonOptions(json);
            // playerProfile.audioOptions.fxVolume;
            // playerProfile.audioOptions.musicVolume;
        }

        public void SaveData()
        {
            StartCoroutine(SaveDataCo());
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
            var json = playerProfileSO.ToJsonGameplay();
            Debug.Log($">>> save {json}");
            PlayerPrefs.SetString("PlayerProfile", json);
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
            LoadPlayerProfileGameplayData();
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

        public void SetLoaderText(string text)
        {
            m_TMP_Text.text = text;
        }
    }

}