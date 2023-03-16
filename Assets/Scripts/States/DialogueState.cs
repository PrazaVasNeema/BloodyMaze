namespace BloodyMaze.States
{
    public class DialogueState : GameStateBehavior
    {
        private void OnDisable()
        {
            if (GameState.current.state == GameStates.INTERACTING)
                GameState.current.ChangeState();
        }
    }
}
