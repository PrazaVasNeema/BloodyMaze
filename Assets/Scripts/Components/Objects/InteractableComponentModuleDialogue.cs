using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class InteractableComponentModuleDialogue : InteractableComponentModuleAbstract
    {
        [SerializeField] private string m_dialogueKey;

        public override void ActivateModule()
        {
            m_flag = false;
            GameState.current.ChangeState();
            GameEvents.OnChangeGameplayState?.Invoke(2);
            while (!m_flag)
            {
            }
            GameEvents.OnStartDialogue?.Invoke(m_dialogueKey);
            m_flag = false;
        }
    }
}
