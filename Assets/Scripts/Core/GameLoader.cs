using System;
using UnityEngine;

namespace BloodyMaze
{
    public class GameLoader : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
            GameController.instance.LoadScene("MainMenu");
        }
    }
}