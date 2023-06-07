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
        private ObjectivesTrackingManager m_objectivesTrackingManager;

        private void Awake()
        {
            Debug.Log("AwakeUIRoot");
            if (TryGetComponent<ObjectivesTrackingManager>(out m_objectivesTrackingManager))
                m_objectivesTrackingManager.Init(m_uiJournal);
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
