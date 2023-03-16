using UnityEngine;

namespace BloodyMaze.States
{
    public class NoteState : GameStateBehavior
    {
        private void OnEnable()
        {
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
            if (GameState.current.state == GameStates.INTERACTING)
                GameState.current.ChangeState();
        }
    }
}
