using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Controllers
{

    public class GameController : MonoBehaviour
    {
        public static GameController instance { get; private set; }

        public PlayerProfileSO playerProfile;
        public CharacterData characterData;
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
        }

        private void Start()
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

        private void OnApplicationQuit()
        {
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
    }

}