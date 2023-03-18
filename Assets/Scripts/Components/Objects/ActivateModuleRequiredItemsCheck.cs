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

        public override void ActivateModule()
        {
            if (!GameInventory.current.InventoryContains(m_requiredItemName))
            {
                Debug.Log("Activation failed");
            }
            GameInventory.current.RemoveItem(m_requiredItemName);
            onSuccesfullActivate?.Invoke();
        }
    }
}
