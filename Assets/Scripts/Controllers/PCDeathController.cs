using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Controllers
{
    public class PCDeathController : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.OnPCDeath += ActivateDeathLogics;
        }

        private void OnDisable()
        {
            GameEvents.OnPCDeath -= ActivateDeathLogics;
        }

        private void ActivateDeathLogics()
        {
            StartCoroutine(DeathLogicsCo());
        }

        IEnumerator DeathLogicsCo()
        {
            GameEvents.OnCallGotoFunction?.Invoke("none");
            ActionStatesManager.SetState(ActionStates.INTERACTING);
            // StartCoroutine(SlowDownTimeCo());
            yield return new WaitForSecondsRealtime(2f);
            // GameController.SetLoaderText("Прошли сутки");
            GameTransitionSystem.ScreenFade();


            yield return new WaitForSecondsRealtime(2);

            GameEvents.OnCallGotoFunction?.Invoke("reload_level");
        }

        IEnumerator SlowDownTimeCo()
        {
            while (Time.timeScale != 0.1f)
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, 0.1f);
                yield return new();
            }
        }
    }
}
