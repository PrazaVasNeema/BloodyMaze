using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class ActivateModuleNote : ActivateModuleAbstract
    {
        [SerializeField] private string m_noteTextKey;
        [SerializeField] private Sprite m_noteBackground;

        public override void ActivateModule()
        {
            m_flag = false;
            ActionStatesManager.ChangeState();
            GameEvents.OnCallGotoFunction?.Invoke("note");

            GameEvents.OnShowNote?.Invoke(m_noteTextKey, m_noteBackground);
            m_flag = false;
            if (!string.IsNullOrEmpty(m_eventFlagToCheck))
                GameEvents.OnEventFlagCheck?.Invoke(m_eventFlagToCheck);
        }


    }
}
