using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Controllers
{
    public class TransitionController : MonoBehaviour
    {
        // public static TransitionController current { private set; get; }

        // private void Awake()
        // {
        //     current = this;
        // }

        // private void OnDestroy()
        // {
        //     current = null;
        // }

        // private IEnumerator LocalBlackenModuleCo()
        // {
        //     ScreenFade();
        //     bool doOnce = true;
        //     while (doOnce)
        //     {
        //         doOnce = false;
        //         yield return new WaitForSeconds(m_fadeInDuration);
        //     }
        //     if (m_shouldChangeRoom)
        //         m_nextRoom.gameObject.SetActive(true);
        //     m_characterComponent.GetComponent<Transform>().position = m_whereTo.transform.position;
        //     if (m_shouldWait)
        //     {
        //         yield return new WaitForSecondsRealtime(1f);
        //         FindObjectOfType<ShowTutorialCo>().ShowTutorial();
        //     }

        //     while (m_shouldWait)
        //     {
        //         yield return new WaitForSeconds(0.1f);
        //     }


        //     GameEvents.OnTransition?.Invoke();
        //     if (m_shouldChangeRoom)
        //         m_prevRoom.gameObject.SetActive(false);
        //     doOnce = true;
        //     while (doOnce)
        //     {
        //         doOnce = false;
        //         yield return new WaitForSeconds(m_fadeOutDuration);
        //     }
        //     ScreenUnfade();
        //     GameEvents.OnCallGotoFunction?.Invoke("gameplay");
        //     GameEvents.OnHideMessage?.Invoke();
        // }
    }
}
