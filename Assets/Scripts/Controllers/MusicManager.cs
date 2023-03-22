using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BloodyMaze.Controllers
{
    public enum MusicStates { MAIN_MENU, EXPLORING, BATTLE, CRITICAL };

    public class MusicManager : MonoBehaviour
    {
        public static MusicManager current { private set; get; }

        [SerializeField] private AudioMixer m_audioMixer;
        [SerializeField] private List<string> m_exposureParams;
        private AudioSource[] m_audioSources;
        private int m_currentParamIndex = 0;
        private Coroutine m_currentJam;
        private bool m_shouldStopJam;

        private void Awake()
        {
            current = this;
            for (int b = 0; b < 2; b++)
            {
                Debug.Log(m_exposureParams[b]);
            }
            m_audioSources = GetComponents<AudioSource>();
            int i = 0;
            foreach (AudioSource audioSource in m_audioSources)
                audioSource.outputAudioMixerGroup = m_audioMixer.FindMatchingGroups("Master")[i++];
            // AudioMixer. a f.
            // a.audioMixer.
            // current = this;
            // AudioSource source;
            // source.outputAudioMixerGroup
        }

        private void OnDestroy()
        {
            current = null;
        }

        public void SetJam(string audioGroupName)
        {
            // if (current.m_currentJam != null)
            // {
            //     current.StopCoroutine(current.m_currentJam);
            //     FadeMixerGroup.StartFade(current.m_audioMixer, current.m_exposureParams[current.m_currentParamIndex], 2f, 0f);
            // }
            m_currentParamIndex = (m_exposureParams.Count + ++m_currentParamIndex) % m_exposureParams.Count;
            MusicGroup musicGroup = GameController.musicGroups.GetGroup(audioGroupName);
            current.StartCoroutine(current.CurrentJamCo(musicGroup));
        }


        IEnumerator CurrentJamCo(MusicGroup musicGroup)
        {
            int minIndex = musicGroup.hasIntro ? 1 : 0;
            int index = musicGroup.shouldPlayRandom ? Random.Range(minIndex, musicGroup.audioClips.Count) : 0;
            while (true)
            {
                int prevIndex = index;
                m_audioSources[m_currentParamIndex].clip = musicGroup.audioClips[index];
                m_audioSources[m_currentParamIndex].Play();
                index = musicGroup.shouldPlayRandom ? Random.Range(minIndex, musicGroup.audioClips.Count) :
                (musicGroup.audioClips.Count + ++index) % musicGroup.audioClips.Count;
                if (index < minIndex)
                    index = minIndex;
                yield return new WaitForSecondsRealtime(musicGroup.audioClips[prevIndex].length);
            }
        }

    }
}
