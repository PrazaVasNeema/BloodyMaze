using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BloodyMaze.States
{
    public class GameplayGameMode : GameModeBehavior
    {
        public void GotoGameplay()
        {
            ChangeState<GameplayState>();
        }

        public void GotoPasuse()
        {
            ChangeState<JournalState>();
        }

        public void GotoMainMenu()
        {
            GameController.LoadScene("MainMenu");
        }

        public void ReloadLevel()
        {
            var scene = SceneManager.GetActiveScene();
            GameController.LoadScene(scene.name);
        }
    }
}
