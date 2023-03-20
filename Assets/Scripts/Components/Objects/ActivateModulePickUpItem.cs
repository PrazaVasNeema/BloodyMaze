using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class ActivateModulePickUpItem : ActivateModuleAbstract
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
            Debug.Log("Check1");
            GameEvents.OnHideMessage?.Invoke();
            Debug.Log("Check2");
            if (!string.IsNullOrEmpty(m_eventFlagToCheck))
                GameController.playerProfile.playerProfileData.globalEventsData.Find((x) => x.eventKey == m_eventFlagToCheck).flag = true;
            Debug.Log("Check3");
            var activateOnInteract = GetComponent<ActivateOnInteract>();
            if (activateOnInteract)
                activateOnInteract.interactComponent.OnInteract -= activateOnInteract.Activate;
            gameObject.SetActive(false);
        }
    }
}
