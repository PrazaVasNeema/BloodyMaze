using UnityEngine;

namespace BloodyMaze.States
{
    public class GameplayState : GameStateBehavior
    {
        private void OnEnable()
        {
            Debug.Log("fghjhghgf");
            ActionStatesManager.SetState(ActionStates.EXPLORING);
            GameEvents.OnEnterGameplayState?.Invoke();
        }
    }
}