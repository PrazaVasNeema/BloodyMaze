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
        [SerializeField] private string activationFailedMiniMessageKey;

        public override void ActivateModule()
        {
            if (!GameInventory.current.InventoryContains(m_requiredItemName) && !string.IsNullOrEmpty(activationFailedMiniMessageKey))
            {
                GameEvents.OnShowMiniMessage?.Invoke(activationFailedMiniMessageKey);
                return;
            }
            GameInventory.current.RemoveItem(m_requiredItemName);
            onSuccesfullActivate?.Invoke();
        }
    }
}
