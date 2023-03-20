using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class CheckIfObjectShouldSpawn : MonoBehaviour
    {
        [SerializeField] private string[] m_eventsShouldBeChecked;
        public string[] eventsShouldBeChecked => m_eventsShouldBeChecked;
        [SerializeField] private string[] m_eventsShouldBeUnchecked;
        public string[] eventsShouldBeUnhecked => m_eventsShouldBeUnchecked;
    }
}
