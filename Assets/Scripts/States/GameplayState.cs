namespace BloodyMaze.States
{
    public class GameplayState : GameStateBehavior
    {
        private void OnEnable()
        {
            GameEvents.OnModeChange?.Invoke("GameplayState");
        }
    }
}