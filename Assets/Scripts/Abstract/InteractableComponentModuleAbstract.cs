using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public abstract class InteractableComponentModuleAbstract : MonoBehaviour
    {
        [SerializeField] private string m_messageToShow;
        public string messageToShow => m_messageToShow;

        public abstract bool Activate();
    }
}
