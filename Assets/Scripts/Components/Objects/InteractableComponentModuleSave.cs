using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class InteractableComponentModuleSave : InteractableComponentModuleAbstract
    {
        public override void ActivateModule()
        {
            GameController.current.SaveData();
        }
    }
}
