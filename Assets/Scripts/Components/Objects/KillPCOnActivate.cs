using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class KillPCOnActivate : MonoBehaviour
    {
        public void Activate()
        {
            HealthComponent PCHealthComponent = FindObjectOfType<CharacterController>().GetComponent<HealthComponent>();
            PCHealthComponent.ChangeHPWithAmount(1000f);
        }
    }
}
