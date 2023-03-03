using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class InteractableComponentModulePickable : InteractableComponentModuleAbstract
    {
        private PickableItemComponent m_objectModel;

        private void Awake()
        {
            m_objectModel = GetComponentInChildren<PickableItemComponent>();
            if (!m_objectModel)
                Destroy(this);
        }

        public override bool Activate()
        {
            GameInventory.current.AddItem(m_objectModel.item);
            Destroy(gameObject);
            GameEvents.OnUIGMessagesChangeState?.Invoke(null);
            return true;
        }
    }
}
