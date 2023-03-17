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
            GameEvents.OnChangeGameplayState += CallGoto;
        }

        private void OnDisable()
        {
            GameEvents.OnChangeGameplayState -= CallGoto;
        }

        private void CallGoto(int stateIndex)
        {
            switch (stateIndex)
            {
                case 0:
                    GotoGameplay();
                    break;
                case 1:
                    GotoNote();
                    break;
                case 2:
                    GotoDialogue();
                    break;
                case 3:
                    GotoJournal();
                    break;
                case 4:
                    GotoNone();
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
