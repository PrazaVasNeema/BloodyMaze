using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BloodyMaze
{
    public class DialogueController : MonoBehaviour
    {
        public Text nameText;
        public Text dialogueText;

        public Animator animator;

        public float typingSpeed = 0.1f;

        private string sentence;

        // Queue - FIFO: First In First Out
        private Queue<string> sentences;

        // Start is called before the first frame update
        void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Debug.Log("Starting conversation with " + dialogue.npcName);

            animator.SetBool("IsOpen", true);

            nameText.text = dialogue.npcName;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            sentence = sentences.Dequeue();
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (dialogueText.text.Length == sentence.Length)
            {
                if (sentences.Count == 0)
                {
                    EndDialogue();
                    return;
                }
                sentence = sentences.Dequeue();
                Debug.Log(sentence);
                StartCoroutine(TypeSentence(sentence));
            }
            else
            {
                dialogueText.text = sentence;
                StopAllCoroutines();
            }
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                //yield return null;
                yield return new WaitForSeconds(typingSpeed);
            }
            Debug.Log("this");
        }

        void EndDialogue()
        {
            Debug.Log("End of conversation");
            GameState.current.ChangeState();
            animator.SetBool("IsOpen", false);
        }
    }
}

