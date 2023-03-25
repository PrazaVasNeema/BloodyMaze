using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace BloodyMaze.AI
{
    [System.Serializable]
    public class DestroySelf : ActionNode
    {
        protected override void OnStart()
        {
            if (ActionStatesManager.current.enemiesTriggered != 0)
            {
                ActionStatesManager.ChangeEnemiesTriggeredCount(-1);
                ActionStatesManager.ChangeState();
            }
            GameObject.Destroy(context.gameObject);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Running;
        }
    }
}