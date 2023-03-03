using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class InteractableComponentModuleNote : InteractableComponentModuleAbstract
    {
        [TextArea(3, 10)]
        [SerializeField] private string m_noteText;

        public override bool Activate()
        {
            GameState.current.ChangeState();
            GameEvents.OnSetInteractState?.Invoke();
            GameEvents.OnUIGMessagesChangeState?.Invoke(m_noteText);
            return true;
        }
    }
}
