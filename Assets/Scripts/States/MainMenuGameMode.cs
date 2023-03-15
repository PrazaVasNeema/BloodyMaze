using UnityEngine;

namespace BloodyMaze.States
{
    public class MainMenuGameMode : GameModeBehavior
    {
        public void LoadLevel()
        {
            GameController.LoadScene("SampleScene");
        }
    }
}