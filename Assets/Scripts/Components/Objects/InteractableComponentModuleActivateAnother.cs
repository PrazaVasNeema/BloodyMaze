using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class InteractableComponentModuleActivateAnother : InteractableComponentModuleAbstract
    {
        [SerializeField] private string m_requiredItemName;
        [SerializeField] private AffectObjectOnActivation[] m_objectsToAffect;
        public override bool Activate()
        {
            GameState.current.ChangeState();
            GameEvents.OnSetInteractState?.Invoke();
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
            foreach (AffectObjectOnActivation oneObject in m_objectsToAffect)
            {
                oneObject.Move();
            }
            Debug.Log("Activated succesfully");
            return true;
        }
    }
}
