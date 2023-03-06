using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;

namespace BloodyMaze.Controllers
{
    public class GameTransitionSystem : MonoBehaviour
    {
        public static GameTransitionSystem current { private set; get; }
        [SerializeField] private float m_fadeInDuration;
        [SerializeField] private float m_fadeOutDuration;
        [SerializeField] private Animator m_animator;
        [SerializeField] private CharacterComponent m_characterComponent;

        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        public void Init(CharacterComponent characterComponent)
        {
            m_characterComponent = characterComponent;
        }

        public void TransitCharacter(Transform whereTo)
        {
            StartCoroutine(InCoroutine(whereTo));
        }

        private IEnumerator InCoroutine(Transform whereTo)
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
            StartCoroutine(OutCoroutine(whereTo));
        }

        private IEnumerator OutCoroutine(Transform whereTo)
        {
            m_characterComponent.transform.position = whereTo.transform.position;
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
