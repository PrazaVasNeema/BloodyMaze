using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;

namespace BloodyMaze.Components
{
    public class DialogueStarterComponent : MonoBehaviour
    {
        [SerializeField] private Dialogue[] m_dialogues;

        public void StartDialogue(int dialogueIndex)
        {
            FindObjectOfType<DialogueController>().StartDialogue(m_dialogues[dialogueIndex]);
        }
    }
}
