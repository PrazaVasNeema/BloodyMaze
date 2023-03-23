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

        private SentenceData sentenceData;
        private string m_flagToCheck;
        private Queue<SentenceData> sentencesData = new Queue<SentenceData>();

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
            Dialogue dialogue = GameController.locData.GetDialogue(key);
            m_flagToCheck = flagToCheck;
            animator.SetBool("IsOpen", true);
            sentencesData.Clear();
            foreach (SentenceData sentence in dialogue.sentencesData)
            {
                Debug.Log(sentence);
                sentencesData.Enqueue(sentence);
            }
            sentenceData = sentencesData.Dequeue();
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            nameText.text = sentenceData.personName;
            if (dialogueText.text.Length == sentenceData.sentence.Length)
            {
                if (sentencesData.Count == 0)
                {
                    StartCoroutine(EndDialogueCo());
                    return;
                }
                sentenceData = sentencesData.Dequeue();
                StartCoroutine(TypeSentence(sentenceData.sentence));
            }
            else
            {
                dialogueText.text = sentenceData.sentence;
                StopAllCoroutines();
            }
        }

        IEnumerator TypeSentence(string sentence)
        {
            nameText.text = sentenceData.personName;
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

