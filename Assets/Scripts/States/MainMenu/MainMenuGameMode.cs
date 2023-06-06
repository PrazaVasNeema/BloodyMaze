using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace BloodyMaze.States
{
    public class MainMenuGameMode : GameModeBehavior
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private GameObject m_zoomInButton;
        [SerializeField] private TMP_Dropdown m_optionsLanguageDropdown;
        [SerializeField] private Slider m_optionsMusicSlider;
        [SerializeField] private Slider m_optionsSFXSlider;
        [SerializeField] private InitMainMenuLocData m_initMainMenuLocData;


        public void ApplyOptionsData()
        {
            GameController.instance.SetDataGameOptions(m_optionsLanguageDropdown.value, m_optionsMusicSlider.value, m_optionsSFXSlider.value);
        }
        private void OnEnable()
        {
            GameController.instance.OnLoadingDataGameOptionsComplete += InitOptions;
            InitOptions();
        }

        private void OnDisable()
        {
            GameController.instance.OnLoadingDataGameOptionsComplete -= InitOptions;
        }

        private void InitOptions()
        {
            GameOptionsData gameOptionsData = GameController.instance.gameOptions.GameOptionsData;
            m_optionsLanguageDropdown.value = gameOptionsData.language;
            m_optionsMusicSlider.value = gameOptionsData.volumeMusic;
            m_optionsSFXSlider.value = gameOptionsData.volumeSFX;
            InitInterfaceLocalization();
        }

        public void ZoomIn()
        {
            m_zoomInButton.SetActive(false);
            m_animator.SetBool("AppearAndDisappear", false);
            m_animator.SetTrigger("ZoomIn");
        }

        public void GotoMainMenuState()
        {
            ChangeState<MainMenuState>();
            m_animator.SetTrigger("GotoMainMenu");
        }

        public void GotoSelectLevelState()
        {
            ChangeState<SelectLevelState>();
            m_animator.SetTrigger("GotoSelectLevel");
        }

        public void GotoChooseSaveFileState(bool shouldStartNewGame)
        {
            GameController.instance.shouldStartNewGame = shouldStartNewGame;
            ChangeState<ChooseSaveFileState>();
        }

        public void GotoSettingState()
        {
            ChangeState<SettingsState>();
            m_animator.SetTrigger("GotoSettings");
        }

        public void LoadLevel(int choosenProfileIndex)
        {
            var levelInfo = GameController.instance.levelsInfo.GetLevel(GameController.instance.playerProfile.playerProfileData.characterSaveData.lastLevelIndex);
            GameController.instance.LoadScene(levelInfo.sceneName, choosenProfileIndex);
        }

        public void OnExitGame()
        {
            Application.Quit();
        }
    }
}