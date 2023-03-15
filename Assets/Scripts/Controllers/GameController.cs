using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BloodyMaze.Controllers;

namespace BloodyMaze
{

    public class GameController : MonoBehaviour
    {
        public static GameController instance { get; private set; }

        [SerializeField] private GameObject m_loader;

        public PlayerProfileSO playerProfile;
        public CharacterData characterDataDefault;
        public GlobalEventsSO globalEventsDefault;
        public bool shouldInitNewData;
        public LevelController levelController;

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

        private void Start()
        {
            //LoadPlayerProfile();
        }

        private void OnApplicationQuit()
        {
        }

        private void LoadPlayerProfile()
        {
            var json = "";
            if (!shouldInitNewData)
            {
                json = PlayerPrefs.GetString("PlayerProfile");
                Debug.Log($">>> load {json}");
            }
            playerProfile.LoadFromJson(json, shouldInitNewData);
            levelController.Init();
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
            var json = playerProfile.ToJson();
            Debug.Log($">>> save {json}");
            PlayerPrefs.SetString("PlayerProfile", json);
        }

        public static void LoadScene(string sceneName)
        {
            instance.StartCoroutine(instance.LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            m_loader.SetActive(true);

            yield return SceneManager.LoadSceneAsync("Empty");

            System.GC.Collect();
            Resources.UnloadUnusedAssets();

            yield return SceneManager.LoadSceneAsync(sceneName);

            m_loader.SetActive(false);
        }
    }

}