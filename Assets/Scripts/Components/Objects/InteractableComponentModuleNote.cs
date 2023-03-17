using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class InteractableComponentModuleNote : InteractableComponentModuleAbstract
    {
        [SerializeField] private string m_noteTextKey;



        public override void ActivateModule()
        {
            m_flag = false;
            GameState.current.ChangeState();
            GameEvents.OnChangeGameplayState?.Invoke(1);
            while (!m_flag)
            {
            }
            GameEvents.OnShowNote?.Invoke(m_noteTextKey);
            m_flag = false;
        }


    }
}
