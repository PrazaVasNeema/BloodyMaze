using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Coven.AI
{

    [System.Serializable]
    public class NextPosition : ActionNode
    {


        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            blackboard.moveToPosition = context.AIPatrolPositionsManager.ChooseNext();
            return State.Success;
        }
    }

}