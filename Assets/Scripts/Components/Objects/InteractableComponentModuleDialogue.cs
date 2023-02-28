using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class InteractableComponentModuleDialogue : InteractableComponentModuleAbstract
    {
        [SerializeField] private Dialogue dialogue;
        private DialogueStarterComponent m_dialogueStarterComponent;

        private void Awake()
        {
            TryGetComponent(out m_dialogueStarterComponent);
        }

        public override bool Activate()
        {
            GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            m_dialogueStarterComponent.StartDialogue(0);
            return true;
        }
    }
}
