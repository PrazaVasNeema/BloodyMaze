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
            GameState.current.ChangeState();
            GameEvents.OnSetInteractState?.Invoke();
            GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            m_dialogueStarterComponent.StartDialogue(0);
        }
    }
}
