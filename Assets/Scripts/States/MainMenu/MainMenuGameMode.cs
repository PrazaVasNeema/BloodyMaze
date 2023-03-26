using UnityEngine;
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
            GameController.instance.OnLoadingDataComplete += InitOptionsAndLoc;
        }

        private void OnDisable()
        {
            GameController.instance.OnLoadingDataComplete -= InitOptionsAndLoc;
        }

        private void InitOptionsAndLoc()
        {
            GameOptionsData gameOptionsData = GameController.gameOptions.GameOptionsData;
            m_optionsLanguageDropdown.value = gameOptionsData.language;
            m_optionsMusicSlider.value = gameOptionsData.volumeMusic;
            m_optionsSFXSlider.value = gameOptionsData.volumeSFX;
            m_initMainMenuLocData.SetLocDataMainMenu();
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
            GameController.shouldStartNewGame = shouldStartNewGame;
            ChangeState<ChooseSaveFileState>();
        }

        public void GotoSettingState()
        {
            ChangeState<SettingsState>();
            m_animator.SetTrigger("GotoSettings");
        }

        public void LoadLevel(int choosenProfileIndex)
        {
            var levelInfo = GameController.levelsInfo.GetLevel(GameController.playerProfile.playerProfileData.characterSaveData.lastLevelIndex);
            GameController.LoadScene(levelInfo.sceneName, choosenProfileIndex);
        }

        public void OnExitGame()
        {
            Application.Quit();
        }
    }
}