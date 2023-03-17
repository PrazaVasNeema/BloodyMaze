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
        private bool m_shouldChangeRoom;

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

        public void TransitCharacter(Transform whereTo, RoomController prevRoom, RoomController nextRoom, bool shouldChangeRoom)
        {
            m_prevRoom = prevRoom;
            m_nextRoom = nextRoom;
            m_shouldChangeRoom = shouldChangeRoom;
            m_whereTo = whereTo;
            StartCoroutine(InCoroutine());
        }

        private IEnumerator InCoroutine()
        {
            Debug.Log("Start");
            m_animator.SetTrigger("Start");
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(m_fadeInDuration);
            }
            if (m_shouldChangeRoom)
                m_nextRoom.gameObject.SetActive(true);
            Debug.Log("Continue");
            m_characterComponent.GetComponent<Transform>().position = m_whereTo.transform.position;
            GameEvents.OnTransition?.Invoke();

            if (m_shouldChangeRoom)
                m_prevRoom.gameObject.SetActive(false);
            m_animator.SetTrigger("End");
            doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(m_fadeOutDuration);
            }
            Debug.Log("End");
            GameEvents.OnChangeGameplayState?.Invoke(0);
            GameEvents.OnHideMessage?.Invoke();
        }

    }
}
