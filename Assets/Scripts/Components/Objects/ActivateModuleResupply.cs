using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class ActivateModuleResupply : ActivateModuleAbstract
    {
        public override void ActivateModule()
        {
            ActivateOnInteract activateOnInteract = GetComponent<ActivateOnInteract>();
            CharacterComponent characterComponent = FindObjectOfType<CharacterComponent>();
            characterComponent.ammunitionComponent.AddAmmo();
            characterComponent.medsComponent.FullUpMeds();
        }
    }
}
