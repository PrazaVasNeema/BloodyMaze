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
                Collider[] colliders = Physics.OverlapSphere(other.transform.position, 100000, m_layersToHelpSaving);
                if (colliders.Length > 0)
                {
                    float minDistance = 10000f;
                    Transform closestObject = null;
                    foreach (Collider collider in colliders)
                    {
                        if (collider.gameObject.active == false)
                            break;
                        float curDistance = (other.transform.position - collider.transform.position).sqrMagnitude;
                        if (curDistance < minDistance)
                        {
                            minDistance = curDistance;
                            closestObject = collider.GetComponent<Transform>();
                        }
                    }
                    Debug.Log("PCSaver2");
                    Debug.Log($"colliders[0].transform.position {colliders[0].transform.position}");
                    ActionStatesManager.SetState(ActionStates.INTERACTING);
                    other.GetComponentInParent<CharacterComponent>().transform.position = closestObject.position;
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
