using UnityEngine;
using UnityEngine.UI;

namespace BloodyMaze.States
{
    public class MainMenuGameMode : GameModeBehavior
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private GameObject m_zoomInButton;

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