using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class ActivateModuleDialogue : ActivateModuleAbstract
    {
        [SerializeField] private string m_dialogueKey;

        public override void ActivateModule()
        {
            m_flag = false;
            ActionStatesManager.SetState(ActionStates.INTERACTING);
            GameEvents.OnCallGotoFunction?.Invoke("dialogue");

            GameEvents.OnStartDialogue?.Invoke(m_dialogueKey, m_eventFlagToCheck);
            m_flag = false;
        }
    }
}
