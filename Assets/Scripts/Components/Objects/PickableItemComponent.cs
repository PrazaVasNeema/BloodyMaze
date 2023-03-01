using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class PickableItemComponent : MonoBehaviour
    {
        [SerializeField] private PickableItem m_item;
        public PickableItem item => m_item;
    }
}
