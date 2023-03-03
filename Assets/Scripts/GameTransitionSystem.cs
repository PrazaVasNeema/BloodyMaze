using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class GameTransitionSystem : MonoBehaviour
    {
        public static GameTransitionSystem current { private set; get; }
        [SerializeField] private float m_fadeInDuration;
        [SerializeField] private float m_fadeOutDuration;
        [SerializeField] private Animator m_animator;

        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        public void TransitCharacter(Transform whereTo)
        {
            StartCoroutine(InCoroutine());
        }

        private IEnumerator InCoroutine()
        {
            GameState.current.ChangeState();
            GameEvents.OnSetInteractState?.Invoke();
            m_animator.SetTrigger("Start");
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(m_fadeInDuration);
            }
            StartCoroutine(OutCoroutine());
        }

        private IEnumerator OutCoroutine()
        {
            m_animator.SetTrigger("End");
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(m_fadeOutDuration);
            }
            GameState.current.ChangeState();
            GameEvents.OnSetInteractState?.Invoke();
        }
    }
}
