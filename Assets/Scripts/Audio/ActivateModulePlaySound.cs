using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    public class ActivateModulePlaySound : ActivateModuleAbstract
    {
        [SerializeField] private List<AudioSource> m_audioSources;
        [SerializeField] private bool m_shouldPlayRandom;
        private int index = 0;

        public override void ActivateModule()
        {
            if (m_audioSources.Count > 0)
            {
                index = m_shouldPlayRandom ? Random.Range(0, m_audioSources.Count) : (m_audioSources.Count + ++index) % m_audioSources.Count;
                m_audioSources[index].Play();
            }
        }
    }
}
