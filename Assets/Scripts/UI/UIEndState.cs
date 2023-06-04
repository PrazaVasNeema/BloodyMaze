using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BloodyMaze.Controllers;

namespace BloodyMaze
{
    [RequireComponent(typeof(AudioSource))]
    public class UIEndState : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_firstSentenceField;
        [SerializeField] private TMP_Text m_secondSentenceField;
        [SerializeField] private TMP_Text m_thirdSentenceField;
        [SerializeField] private GameObject m_exitButton;
        [SerializeField] private GameObject m_continueButton;
        [SerializeField] private AudioClip[] m_audipClipToPlayWhileTyping = new AudioClip[3];
        [SerializeField] private AudioClip m_audioClipSingleType;
        [SerializeField] private string[] m_sentencesOpenTextFieldsLocKeys = new string[3];
        [SerializeField] private float m_sentencesTypingSpeed = .02f;

        private AudioSource m_audioSource;
        private bool m_shouldContinue = false;

        private void OnEnable()
        {
            Debug.Log("Check");
            ActivateEndGameLogics();
        }

        private void ActivateEndGameLogics()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.clip = m_audipClipToPlayWhileTyping[0];
            StartCoroutine(EndGameCo());
        }

        IEnumerator EndGameCo()
        {
            m_exitButton.GetComponentInChildren<TMP_Text>().text = GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[3]);
            m_continueButton.GetComponentInChildren<TMP_Text>().text = GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[4]);
            m_firstSentenceField.text = "";
            m_secondSentenceField.text = "";
            m_thirdSentenceField.text = "";
            List<List<string>> peopleNamesByLivingStatus = GameController.locData.GetPeopleNamesSortByLivingStatus();
            string textToType;
            // StartCoroutine(SlowDownTimeCo());
            yield return new WaitForSecondsRealtime(2f);
            textToType = $"{GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[0])}: {FormNamesStringWithCommas(peopleNamesByLivingStatus[0])}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_firstSentenceField, textToType, m_audioSource, m_sentencesTypingSpeed));
            m_audioSource.clip = m_audipClipToPlayWhileTyping[1];
            yield return new WaitForSecondsRealtime(1f);
            textToType = $"{GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[1])}: {FormNamesStringWithCommas(peopleNamesByLivingStatus[1])}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_secondSentenceField, textToType, m_audioSource, m_sentencesTypingSpeed));
            m_audioSource.clip = m_audipClipToPlayWhileTyping[2];
            yield return new WaitForSecondsRealtime(2f);
            textToType = $"{GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[2])}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_thirdSentenceField, textToType, m_audioSource, m_sentencesTypingSpeed));
            yield return new WaitForSecondsRealtime(2f);
            m_audioSource.clip = m_audioClipSingleType;
            m_audioSource.loop = false;
            m_audioSource.Play();
            m_thirdSentenceField.text += "?";

            yield return new WaitForSecondsRealtime(2f);

            m_continueButton.SetActive(true);

            while (!m_shouldContinue)
            {
                yield return new WaitForSecondsRealtime(.5f);
            }

            m_continueButton.SetActive(false);
            m_firstSentenceField.text = "";
            m_secondSentenceField.text = "";
            m_thirdSentenceField.text = "";

            m_firstSentenceField.fontSize = 70f;
            m_thirdSentenceField.fontSize = 70f;
            m_sentencesTypingSpeed = .05f;
            m_audioSource.clip = m_audipClipToPlayWhileTyping[0];
            textToType = $"{GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[5])}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_firstSentenceField, textToType, m_audioSource, m_sentencesTypingSpeed));

            m_audioSource.clip = m_audipClipToPlayWhileTyping[0];
            yield return new WaitForSecondsRealtime(1f);

            textToType = $"{GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[6])}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_thirdSentenceField, textToType, m_audioSource, m_sentencesTypingSpeed));

            yield return new WaitForSecondsRealtime(2f);

            m_exitButton.SetActive(true);
        }

        private string FormNamesStringWithCommas(List<string> peopleNames)
        {
            string namesByComma = "";
            for (int i = 0; i < peopleNames.Count; i++)
            {
                namesByComma += i != peopleNames.Count - 1 ? $"{peopleNames[i]}, " : $"{peopleNames[i]}.";
            }
            return namesByComma;
        }

        public void SetShouldContinue()
        {
            m_shouldContinue = true;
        }
    }
}
