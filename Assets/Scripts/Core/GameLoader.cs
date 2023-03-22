using System;
using UnityEngine;

namespace BloodyMaze
{
    public class GameLoader : MonoBehaviour
    {
        private void Start()
        {
            GameController.LoadScene("MainMenu");
        }
    }
}