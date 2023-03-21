using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Controllers
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_introMusic;
        [SerializeField] private AudioSource m_mainMenuMusic;
        [SerializeField] private AudioSource m_situationCritical;
        [SerializeField] private List<AudioSource> m_gameplayExploringMusic;
        [SerializeField] private List<AudioSource> m_gameplayBattleMusic;
    }
}
