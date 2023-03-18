using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{
    public class ActivateModuleRequiredItemsCheck : ActivateModuleAbstract
    {
        [SerializeField] private string m_requiredItemName;
        [SerializeField] private UnityEvent onSuccesfullActivate;
        [SerializeField] private string activationFailedMessage;

        public override void ActivateModule()
        {
            if (!GameInventory.current.InventoryContains(m_requiredItemName) && !string.IsNullOrEmpty(activationFailedMessage))
            {
                GameEvents.OnShowMiniMessage?.Invoke(activationFailedMessage);
                return;
            }
            GameInventory.current.RemoveItem(m_requiredItemName);
            onSuccesfullActivate?.Invoke();
        }
    }
}
