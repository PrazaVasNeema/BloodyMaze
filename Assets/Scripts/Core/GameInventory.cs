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
        }

        private void OnDestroy()
        {
            current = null;
        }

        public void AddItem(PickableItem item)
        {
            m_inventory.Add(item.name, item);
            onInventoryChange?.Invoke(item.name, item);
            Debug.Log(m_inventory[item.name].name);
        }

        public void RemoveItem(string name)
        {
            m_inventory.Remove(name);
            onInventoryChange?.Invoke(name, null);
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

    }
}
