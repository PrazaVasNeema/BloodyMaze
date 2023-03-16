using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class InteractableComponentModuleDialogue : InteractableComponentModuleAbstract
    {
        [SerializeField] private Dialogue dialogue;
        private DialogueStarterComponent m_dialogueStarterComponent;

        private void Awake()
        {
            TryGetComponent(out m_dialogueStarterComponent);
        }

        public override void ActivateModule()
        {
            m_flag = false;
            GameState.current.ChangeState();
            GameEvents.OnChangeGameplayState?.Invoke(2);
            while (!m_flag)
            {
            }
            m_dialogueStarterComponent.StartDialogue(0);
            m_flag = false;
        }
    }
}
