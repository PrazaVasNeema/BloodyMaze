using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class PickableItemComponent : MonoBehaviour
    {
        [SerializeField] private PickableItem m_item;
        public PickableItem item => m_item;
        [SerializeField] private int m_correspondingFlag;
        public int correspondingFlag => m_correspondingFlag;

        public void SetItem(PickableItem item)
        {
            m_item = item;
        }
    }
}
