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
        private bool m_shouldWait = false;

        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        public static void Init(CharacterComponent characterComponent)
        {
            current.m_characterComponent = characterComponent;
        }

        public static void ScreenFade()
        {
            Debug.Log("ScreenFade");
            current.m_animator.SetTrigger("Start");
        }

        public static void ScreenUnfade()
        {
            Debug.Log("ScreenUnfade");
            current.m_animator.SetTrigger("End");
        }

        public void TransitCharacter(Transform whereTo, RoomController prevRoom, RoomController nextRoom, bool shouldChangeRoom)
        {
            m_prevRoom = prevRoom;
            m_nextRoom = nextRoom;
            m_shouldChangeRoom = shouldChangeRoom;
            m_whereTo = whereTo;
            StartCoroutine(InCoroutine());
        }

        public void TransitCharacter(Transform whereTo, RoomController prevRoom, RoomController nextRoom, bool shouldChangeRoom, bool shouldWait)
        {
            m_shouldWait = true;
            m_prevRoom = prevRoom;
            m_nextRoom = nextRoom;
            m_shouldChangeRoom = shouldChangeRoom;
            m_whereTo = whereTo;
            StartCoroutine(InCoroutine());
        }



        private IEnumerator InCoroutine()
        {
            ScreenFade();
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(m_fadeInDuration);
            }
            if (m_shouldChangeRoom)
                m_nextRoom.gameObject.SetActive(true);
            m_characterComponent.GetComponent<Transform>().position = m_whereTo.transform.position;
            if (m_shouldWait)
            {
                yield return new WaitForSecondsRealtime(1f);
                FindObjectOfType<ShowTutorialCo>().ShowTutorial();
            }

            while (m_shouldWait)
            {
                yield return new WaitForSeconds(0.1f);
            }


            GameEvents.OnScreenBlacken?.Invoke();
            if (m_shouldChangeRoom)
                m_prevRoom.gameObject.SetActive(false);
            doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSeconds(m_fadeOutDuration);
            }
            ScreenUnfade();
            GameEvents.OnCallGotoFunction?.Invoke("gameplay");
            GameEvents.OnHideMessage?.Invoke();
        }

        public void CheckShouldWaitFalse()
        {
            m_shouldWait = false;
            FindObjectOfType<ShowTutorialCo>().HideTutorial();
        }
    }
}
