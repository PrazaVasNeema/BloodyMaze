using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(fileName = "MusicGroupSO", menuName = "BloodyMaze/MusicGroupSO")]
    [System.Serializable]
    public class MusicGroupsSO : ScriptableObject
    {

        public List<MusicGroup> m_musicGroups;

        public MusicGroup GetGroup(string key)
        {
            Debug.Log(m_musicGroups.Find((x) => x.key == key).key);
            return m_musicGroups.Find((x) => x.key == key);

        }
    }

    [System.Serializable]
    public class MusicGroup
    {
        public string key;
        public List<AudioClip> audioClips;
        public bool shouldPlayRandom;
        public bool hasIntro;
    }
}
