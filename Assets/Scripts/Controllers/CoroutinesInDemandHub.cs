using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.UI;

namespace BloodyMaze
{
    public class CoroutinesInDemandHub : MonoBehaviour
    {
        public static CoroutinesInDemandHub instance { private set; get; }

        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void WaitForExploringState(UIJournal uiJournal, string newObjectiveText = null)
        {
            StartCoroutine(WaitForExploringStateCo(uiJournal, newObjectiveText));
        }

        private IEnumerator WaitForExploringStateCo(UIJournal uiJournal, string newObjectiveText = null)
        {
            while (ActionStatesManager.state != ActionStates.EXPLORING)
            {
                yield return new WaitForSecondsRealtime(0.1f);
            }
            uiJournal.UpdateObjective(newObjectiveText);
        }
    }
}
