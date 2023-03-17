using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

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

        public override void ActivateModule()
        {
            GameInventory.current.AddItem(m_objectModel.item);
            GameController.instance.playerProfileSO.playerProfileData.globalEventsData[m_objectModel.correspondingFlag].flag = true;
            Destroy(gameObject);
            GameEvents.OnHideMessage?.Invoke();
        }
    }
}
