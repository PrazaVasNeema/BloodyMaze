using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BloodyMaze.States
{
    public class GameplayGameMode : GameModeBehavior
    {
        private void OnEnable()
        {
            GameEvents.OnCallGotoFunction += CallGoto;
        }

        private void OnDisable()
        {
            GameEvents.OnCallGotoFunction -= CallGoto;
        }

        /// <summary>
        /// States: gameplay, note, dialogue,
        /// journal, none, reload_level,
        /// main_menu
        /// </summary>
        /// <param name="stateName"></param>
        private void CallGoto(string stateName)
        {
            switch (stateName)
            {
                case "gameplay":
                    GotoGameplay();
                    break;
                case "note":
                    GotoNote();
                    break;
                case "dialogue":
                    GotoDialogue();
                    break;
                case "journal":
                    GotoJournal();
                    break;
                case "none":
                    GotoNone();
                    break;
                case "end_game":
                    GotoEndGame();
                    break;
                case "main_menu":
                    ReloadLevel();
                    break;
                case "reload_level":
                    ReloadLevel();
                    break;
            }
        }

        public void GotoGameplay()
        {
            ChangeState<GameplayState>();
        }

        public void GotoNote()
        {
            ChangeState<NoteState>();
        }
        public void GotoDialogue()
        {
            ChangeState<DialogueState>();
        }

        public void GotoJournal()
        {
            ChangeState<JournalState>();
        }

        public void GotoNone()
        {
            ChangeState<NoneState>();
        }

        public void GotoEndGame()
        {
            ChangeState<EndGameState>();
        }

        public void GotoMainMenu()
        {
            GameController.LoadScene("MainMenu");
        }

        public void ReloadLevel()
        {

            GameController.LoadScene("LevelPreLoader", isReloaded: true);
        }
    }
}
