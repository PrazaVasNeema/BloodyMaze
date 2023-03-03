using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class InteractableComponentModuleActivateAnother : InteractableComponentModuleAbstract
    {
        [SerializeField] private string m_requiredItemName;
        [SerializeField] private UnityEvent onSuccesfullActivate;

        public override bool Activate()
        {
            GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            if (m_requiredItemName != "")
            {
                if (!GameInventory.current.InventoryContains(m_requiredItemName))
                {
                    Debug.Log("Activation failed");
                    return false;
                }
                GameInventory.current.RemoveItem(m_requiredItemName);
            }
            onSuccesfullActivate?.Invoke();
            Debug.Log("Activated succesfully");
            return true;
        }
    }
}
