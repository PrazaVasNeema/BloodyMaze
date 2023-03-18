using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class InteractableComponentModulePickable : ActivateModuleAbstract
    {
        private PickableItemComponent m_item;
        public PickableItemComponent item => m_item;

        private void Awake()
        {
            m_item = GetComponentInChildren<PickableItemComponent>();
            if (!m_item)
                Destroy(this);
        }

        public override void ActivateModule()
        {
            GameInventory.current.AddItem(m_item.item);
            GameController.instance.playerProfileSO.playerProfileData.globalEventsData.Find((x) => x.eventKey == item.correspondingEventKey).flag = true;
            Destroy(gameObject);
            GameEvents.OnHideMessage?.Invoke();
        }
    }
}
