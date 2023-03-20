namespace BloodyMaze.States
{
    public class GameplayState : GameStateBehavior
    {
        private void OnEnable()
        {
            GameEvents.OnEnterGameplayState?.Invoke();
            ActionStatesManager.SetState(ActionStates.EXPLORING);
        }
    }
}