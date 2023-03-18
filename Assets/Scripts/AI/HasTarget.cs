using TheKiwiCoder;

namespace BloodyMaze.AI
{
    public class HasTarget : DecoratorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (!blackboard.target && !blackboard.targetPreviousSeenAt)
            {
                return State.Failure;
            }
            return child.Update();
        }
    }
}