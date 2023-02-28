using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
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
