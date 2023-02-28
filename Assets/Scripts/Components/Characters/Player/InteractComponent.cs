using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class InteractComponent : MonoBehaviour
    {
        public System.Action OnInteract;

        public void Interact()
        {
            OnInteract?.Invoke();
        }
    }
}
