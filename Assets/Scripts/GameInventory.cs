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
        }

        public void RemoveItem(PickableItem item)
        {
            m_inventory.Remove(item.name);
            onInventoryChange?.Invoke(item.name, null);
        }

    }
}
