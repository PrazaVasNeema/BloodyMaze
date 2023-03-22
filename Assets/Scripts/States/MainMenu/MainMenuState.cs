using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public class MainMenuState : GameStateBehavior
    {
        public void LoadLevel()
        {
            GameController.LoadScene("SampleScene");
        }


    }
}
