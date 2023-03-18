using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
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
            GameController.SetLoaderText("Прошли сутки");
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSecondsRealtime(2);
            }
            GameEvents.OnCallGotoFunction?.Invoke("reload_level");
        }
    }
}
