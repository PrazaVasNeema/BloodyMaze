using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class InteractableComponentModuleNote : ActivateModuleAbstract
    {
        [SerializeField] private string m_noteTextKey;



        public override void ActivateModule()
        {
            m_flag = false;
            ActionStatesManager.current.ChangeState();
            GameEvents.OnCallGotoFunction?.Invoke(1);
            while (!m_flag)
            {
            }
            GameEvents.OnShowNote?.Invoke(m_noteTextKey);
            m_flag = false;
        }


    }
}
