using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BloodyMaze.States;

namespace BloodyMaze
{
    public class UIDialogue : MonoBehaviour
    {
        public Text nameText;
        public Text dialogueText;
        public Animator animator;
        public float typingSpeed = 0.1f;

        private string sentence;
        private string m_flagToCheck;
        private Queue<string> sentences = new Queue<string>();

        private void OnEnable()
        {
            GameEvents.OnStartDialogue += StartDialogue;
        }

        private void OnDisable()
        {
            GameEvents.OnStartDialogue -= StartDialogue;
        }

        public void StartDialogue(string key, string flagToCheck)
        {
            Dialogue dialogue = GameController.instance.locData.GetDialogue(key);
            m_flagToCheck = flagToCheck;
            animator.SetBool("IsOpen", true);
            nameText.text = dialogue.npcName;
            sentences.Clear();
            foreach (string sentence in dialogue.sentences)
            {
                Debug.Log(sentence);
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
                    StartCoroutine(EndDialogueCo());
                    return;
                }
                sentence = sentences.Dequeue();
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
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        IEnumerator EndDialogueCo()
        {
            animator.SetBool("IsOpen", false);
            bool doOnce = false;
            if (doOnce)
            {
                doOnce = true;
                yield return new WaitForSecondsRealtime(1f);
            }
            if (!string.IsNullOrEmpty(m_flagToCheck))
                GameEvents.OnEventFlagCheck?.Invoke(m_flagToCheck);
            GameEvents.OnCallGotoFunction("gameplay");
        }
    }
}

