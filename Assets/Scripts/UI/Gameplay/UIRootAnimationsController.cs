using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.UI;

namespace BloodyMaze.Controllers
{
    public class UIRootAnimationsController : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private UIJournal m_uiJournal;
        [SerializeField] private float m_miniMessageDisplayTime = 3f;

        private void Start()
        {
            Debug.Log("AwakeUIRoot");
            if (GameController.instance.playerProfile.playerProfileData.profileSpecificGameOptionsData.shouldShowObjectiveTracking)
            {
                Debug.Log("AwakeUIRoot2");
                m_uiJournal.InitObjectiveTracking();
                Debug.Log("AwakeUIRoot3");
            }
        }

        public void ShowMiniMessageAnim(string miniMessageKey)
        {
            Debug.Log("MiniMessage");
            m_animator.SetBool("MiniMessageShouldBeShown", true);
            StopCoroutine(HideMiniMessage());
            StartCoroutine(HideMiniMessage());
        }

        IEnumerator HideMiniMessage()
        {
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSecondsRealtime(m_miniMessageDisplayTime);
            }
            m_animator.SetBool("MiniMessageShouldBeShown", false);
        }

        public void FadeScreen()
        {
            m_animator.SetTrigger("Start");
        }

        public void UnfadeScreen()
        {
            m_animator.SetTrigger("End");
        }

        public void FadeScreenExtra()
        {
            ActionStatesManager.SetState(ActionStates.INTERACTING);
            GameEvents.OnCallGotoFunction.Invoke("none");
            m_animator.SetTrigger("Start");
        }

        public void UnfadeScreenExtra()
        {
            m_animator.SetTrigger("End");
            StartCoroutine(UnfadeScreenExtraCO());
        }

        private IEnumerator UnfadeScreenExtraCO()
        {
            yield return new WaitForSecondsRealtime(2f);
            ActionStatesManager.SetState(ActionStates.EXPLORING);
            GameEvents.OnCallGotoFunction.Invoke("gameplay");
        }

        public void SetShowTutorialState(bool flagToSet)
        {
            m_animator.SetBool("ShouldShowTutorial", flagToSet);
            if (!flagToSet)
            {
                UnfadeScreenExtra();
            }
        }

        public void CallOnScreenBlacken()
        {
            GameEvents.OnScreenBlacken?.Invoke();
        }
    }
}
