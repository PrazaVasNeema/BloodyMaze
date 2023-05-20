using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class GameInventory : MonoBehaviour
    {
        public static GameInventory current { private set; get; }

        private Dictionary<string, PickableItem> m_inventory = new Dictionary<string, PickableItem>();
        public System.Action<string, PickableItem> onInventoryChange;

        private void Awake()
        {
            current = this;
            GameEvents.OnInitLevelComplete += InitHudInventoryDisplay;
        }

        private void OnDestroy()
        {
            current = null;
        }

        public bool AddItem(PickableItem item)
        {
            if (m_inventory.ContainsKey(item.name))
                return false;
            m_inventory.Add(item.name, item);
            onInventoryChange?.Invoke(item.name, item);
            Debug.Log(m_inventory[item.name].name);
            return true;
        }

        public void RemoveItem(string name)
        {
            onInventoryChange?.Invoke(name, null);
            // m_inventory.Remove(name);
        }

        public bool InventoryContains(string name)
        {
            PickableItem test;
            if (m_inventory.TryGetValue(name, out test))
            {
                return true;
            }
            return false;
        }

        private void InitHudInventoryDisplay()
        {
            foreach (KeyValuePair<string, PickableItem> pickableItem in m_inventory)
            {
                onInventoryChange?.Invoke(pickableItem.Value.name, pickableItem.Value);
            }
            GameEvents.OnInitLevelComplete -= InitHudInventoryDisplay;
        }

    }
}
