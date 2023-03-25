using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BloodyMaze.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_firstSentenceField;
        [SerializeField] private TMP_Text m_secondSentenceField;
        [SerializeField] private GameObject m_exitButton;
        [SerializeField] private AudioClip m_audipClipToPlay;

        private AudioSource m_audioSource;

        private void ActivateEndGameLogics()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.clip = m_audipClipToPlay;
            StartCoroutine(EndGameCo());
        }

        IEnumerator EndGameCo()
        {
            string textToType;
            GameEvents.OnCallGotoFunction?.Invoke("none");
            GameTransitionSystem.ScreenFade();
            ActionStatesManager.SetState(ActionStates.INTERACTING);
            // StartCoroutine(SlowDownTimeCo());
            yield return new WaitForSecondsRealtime(2f);
            textToType = $"Суток прошло: {GameController.playerProfile.playerProfileData.characterSaveData.dayNum}";
            yield return StartCoroutine(TypeSentence.TypeSentenceStatic(m_firstSentenceField, textToType, m_audioSource, 0.02f));
            yield return new WaitForSecondsRealtime(1f);
            textToType = $"Человек спасено: {5 - GameController.playerProfile.playerProfileData.characterSaveData.dayNum + 1}";
            yield return new WaitForSecondsRealtime(2);
            m_exitButton.SetActive(true);
        }
    }
}
