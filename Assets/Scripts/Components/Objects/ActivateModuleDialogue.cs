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
            ActionStatesManager.ChangeState();
            GameEvents.OnCallGotoFunction?.Invoke("dialogue");
            while (!m_flag)
            {
            }
            GameEvents.OnStartDialogue?.Invoke(m_dialogueKey, m_eventFlagToCheck);
            m_flag = false;
        }
    }
}
