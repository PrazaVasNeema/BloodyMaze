using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;

namespace BloodyMaze
{
    public class PCSaver : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layersToSave;
        [SerializeField] private LayerMask m_layersToHelpSaving;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("PCSaver0");
            if (m_layersToSave.value == 1 << other.gameObject.layer)
            {
                Debug.Log("PCSaver1");
                Collider[] colliders = Physics.OverlapSphere(transform.position, 100000, m_layersToHelpSaving);
                if (colliders.Length > 0)
                {
                    Debug.Log("PCSaver2");
                    Debug.Log($"colliders[0].transform.position {colliders[0].transform.position}");
                    ActionStatesManager.SetState(ActionStates.INTERACTING);
                    other.GetComponentInParent<CharacterComponent>().transform.position = colliders[0].transform.position;
                    StartCoroutine(WaitAndSetExploringCo());
                }
                Debug.Log("PCSaver3");
            }
            Debug.Log("PCSaver4");
        }

        IEnumerator WaitAndSetExploringCo()
        {
            yield return new WaitForSecondsRealtime(1f);
            ActionStatesManager.SetState(ActionStates.EXPLORING);
        }
    }
}
