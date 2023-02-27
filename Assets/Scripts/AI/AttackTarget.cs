using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace BloodyMaze.AI
{
    public class AttackTarget : ActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            context.abilitiesManager.UseAbility();
            return State.Running;
        }
    }
}
