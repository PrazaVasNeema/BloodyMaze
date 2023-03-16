namespace BloodyMaze.States
{
    public class DialogueState : GameStateBehavior
    {
        private void OnDisable()
        {
            GameState.current.ChangeState();
        }
    }
}
