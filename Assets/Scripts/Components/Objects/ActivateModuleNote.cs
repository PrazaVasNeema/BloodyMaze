using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class ActivateModuleNote : ActivateModuleAbstract
    {
        [SerializeField] private string m_noteTextKey;

        public override void ActivateModule()
        {
            m_flag = false;
            ActionStatesManager.ChangeState();
            GameEvents.OnCallGotoFunction?.Invoke("note");
            while (!m_flag)
            {
            }
            GameEvents.OnShowNote?.Invoke(m_noteTextKey);
            m_flag = false;
            if (!string.IsNullOrEmpty(m_eventFlagToCheck))
                GameEvents.OnEventFlagCheck?.Invoke(m_eventFlagToCheck);
        }


    }
}
