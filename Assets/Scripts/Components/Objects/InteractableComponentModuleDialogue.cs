using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class InteractableComponentModuleDialogue : ActivateModuleAbstract
    {
        [SerializeField] private string m_dialogueKey;
        [SerializeField] private string m_flagToCheck;

        public override void ActivateModule()
        {
            m_flag = false;
            ActionStatesManager.current.ChangeState();
            GameEvents.OnCallGotoFunction?.Invoke(2);
            while (!m_flag)
            {
            }
            GameEvents.OnStartDialogue?.Invoke(m_dialogueKey, m_flagToCheck);
            m_flag = false;
        }
    }
}
