using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class InteractableComponentModuleUseItems : InteractableComponentModuleAbstract
    {
        [SerializeField] private string m_requiredItemName;

        public override bool Activate()
        {
            GameState.current.ChangeState();
            GameEvents.OnSetInteractState?.Invoke();
            GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            {
                if (GameInventory.current.InventoryContains(m_requiredItemName))
                {
                    Debug.Log("Activated succesfully");
                    GameInventory.current.RemoveItem(m_requiredItemName);
                    return true;
                }
                else
                    Debug.Log("Activation failed");
            }
            return false;
        }
    }
}
