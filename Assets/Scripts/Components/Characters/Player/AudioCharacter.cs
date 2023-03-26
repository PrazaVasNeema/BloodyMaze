using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioCharacter : MonoBehaviour
    {
        [SerializeField] AudioClip m_walkSound;
        [SerializeField] AudioClip m_shootSound;
        [SerializeField] AudioClip m_reloadSound;
        [SerializeField] AudioClip m_drumIsEmptySound;

        private AbilitiesManager m_abilitiesManager;
        private MovementComponentCharacter m_movementComponentCharacter;
        private AudioSource m_audioSourceWalk;
        private AudioSource m_audioSourceOther;

        private bool m_doOnceWalking;

        private void Awake()
        {
            m_abilitiesManager = GetComponentInParent<AbilitiesManager>();
            m_movementComponentCharacter = GetComponentInParent<MovementComponentCharacter>();
            AudioSource[] audioSources = GetComponents<AudioSource>();
            m_audioSourceWalk = audioSources[0];
            m_audioSourceOther = audioSources[1];
        }

        private void Start()
        {
            m_audioSourceWalk.clip = m_walkSound;
            m_audioSourceWalk.Play();
        }

        private void OnEnable()
        {
            m_abilitiesManager.onUseAbility.AddListener(OnShootRoundHandler);
        }

        private void OnDisable()
        {
            m_abilitiesManager.onUseAbility.RemoveListener(OnShootRoundHandler);
        }



        private void OnShootRoundHandler()
        {
            m_audioSourceOther.clip = m_shootSound;
            m_audioSourceOther.Play();
        }

        public void OnReloadDrumHandler()
        {
            m_audioSourceOther.clip = m_reloadSound;
            m_audioSourceOther.Play();
        }

        public void OnDrumIsEmpty()
        {
            m_audioSourceOther.clip = m_drumIsEmptySound;
            m_audioSourceOther.Play();
        }

        private void LateUpdate()
        {
            // transform.LookAt(new Vector3(1, transform.position.y, 1));
            // transform.LookAt(m_currentTarget.transform);
            var velocity = Mathf.Clamp01(m_movementComponentCharacter.velocity / m_movementComponentCharacter.speed);
            if (velocity >= .1f)
            {
                m_audioSourceWalk.UnPause();
                m_doOnceWalking = true;
            }
            else
            {
                m_audioSourceWalk.Pause();
                m_doOnceWalking = false;
            }
        }



    }
}
