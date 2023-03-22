using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class ActivateModulePlaySound : ActivateModuleAbstract
    {
        [SerializeField] private List<AudioClip> m_audioClips;
        [SerializeField] private bool m_shouldPlayRandom;
        private int index = 0;
        private AudioSource m_audioSource;

        private void Awake()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
        }

        public override void ActivateModule()
        {
            if (m_audioClips.Count > 0)
            {
                index = m_shouldPlayRandom ? Random.Range(0, m_audioClips.Count) : (m_audioClips.Count + ++index) % m_audioClips.Count;
                m_audioSource.clip = m_audioClips[index];
                m_audioSource.Play();
            }
        }
    }
}
