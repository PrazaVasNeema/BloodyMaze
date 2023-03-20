using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class PickableItemComponent : MonoBehaviour
    {
        [SerializeField] private PickableItem m_item;
        public PickableItem item => m_item;

        public void SetItem(PickableItem item)
        {
            m_item = new PickableItem(item.name, item.displaySprite, item.modelPrefab);
        }
    }
}
