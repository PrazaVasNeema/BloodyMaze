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

        private RoomController m_prevRoom;
        private RoomController m_nextRoom;
        private Transform m_whereTo;

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

        public void TransitCharacter(Transform whereTo, RoomController prevRoom, RoomController nextRoom)
        {
            m_prevRoom = prevRoom;
            m_nextRoom = nextRoom;
            m_whereTo = whereTo;
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
            m_nextRoom.gameObject.SetActive(true);
            m_characterComponent.GetComponent<Transform>().position = m_whereTo.transform.position;
            m_prevRoom.gameObject.SetActive(false);
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
