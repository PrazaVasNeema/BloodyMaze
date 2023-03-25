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
        [SerializeField] private AudioClip m_audipClipToPlayWhileTyping;
        [SerializeField] private AudioClip m_audioClipSingleType;
        [SerializeField] private string[] m_sentencesOpenTextFieldsLocKeys = new string[3];
        [SerializeField] private float m_sentencesTypingSpeed = .02f;

        private AudioSource m_audioSource;

        private void OnEnable()
        {
            ActivateEndGameLogics();
        }

        private void ActivateEndGameLogics()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.clip = m_audipClipToPlayWhileTyping;
            StartCoroutine(EndGameCo());
        }

        IEnumerator EndGameCo()
        {
            m_firstSentenceField.text = "";
            m_secondSentenceField.text = "";
            m_thirdSentenceField.text = "";
            List<List<string>> peopleNamesByLivingStatus = GameController.locData.GetPeopleNamesSortByLivingStatus();
            string textToType;
            GameTransitionSystem.ScreenFade();
            // StartCoroutine(SlowDownTimeCo());
            yield return new WaitForSecondsRealtime(2f);
            textToType = $"{GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[0])}: {FormNamesStringWithCommas(peopleNamesByLivingStatus[0])}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_firstSentenceField, textToType, m_audioSource, m_sentencesTypingSpeed));
            yield return new WaitForSecondsRealtime(1f);
            textToType = $"{GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[1])}: {FormNamesStringWithCommas(peopleNamesByLivingStatus[1])}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_secondSentenceField, textToType, m_audioSource, m_sentencesTypingSpeed));
            yield return new WaitForSecondsRealtime(2f);
            textToType = $"{GameController.locData.GetInterfaceText(m_sentencesOpenTextFieldsLocKeys[2])}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_thirdSentenceField, textToType, m_audioSource, m_sentencesTypingSpeed));
            yield return new WaitForSecondsRealtime(2f);
            m_audioSource.clip = m_audioClipSingleType;
            m_audioSource.Play();
            m_thirdSentenceField.text += "?";

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
    }
}
