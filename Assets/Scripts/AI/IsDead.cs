using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace BloodyMaze.AI
{
    [System.Serializable]
    public class IsDead : DecoratorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return !context.healthComponent.isDead ? State.Failure : child.Update();
        }
    }
}