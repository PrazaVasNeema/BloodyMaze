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
            GameEvents.OnChangeGameplayState?.Invoke(4);
            GameState.current.SetState(GameStates.INTERACTING);
            GameController.instance.SetLoaderText("Прошли сутки");
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSecondsRealtime(2);
            }
            GameEvents.OnChangeGameplayState?.Invoke(5);
        }
    }
}
