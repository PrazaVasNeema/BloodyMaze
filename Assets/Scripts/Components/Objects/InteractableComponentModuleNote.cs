using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class InteractableComponentModuleNote : InteractableComponentModuleAbstract
    {
        [TextArea(3, 10)]
        [SerializeField] private string m_noteText;



        public override void ActivateModule()
        {
            m_flag = false;
            GameState.current.ChangeState();
            GameEvents.OnChangeGameplayState?.Invoke(1);
            while (!m_flag)
            {
            }
            GameEvents.OnShowNote?.Invoke(m_noteText);
            m_flag = false;
        }


    }
}
