namespace BloodyMaze.States
{
    public class DialogueState : GameStateBehavior
    {
        private void OnDisable()
        {
            if (ActionStatesManager.current.state == ActionStates.INTERACTING)
                ActionStatesManager.ChangeState();
        }
    }
}
