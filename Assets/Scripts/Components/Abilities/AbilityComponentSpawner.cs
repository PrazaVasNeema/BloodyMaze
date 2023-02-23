using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    public class AbilityComponentSpawner : MonoBehaviour, IAbilityComponent
    {
        [SerializeField] private SpawnableComponentAbstract m_spawnableComponentPrefab;
        [SerializeField] private float m_damage;
        [SerializeField] private bool m_spawnableShouldBeLinked;

        private SpawnableComponentAbstract m_spawnableComponentInstance;

        public void UseAbility(float optionalParameter)
        {
            m_spawnableComponentInstance = m_spawnableShouldBeLinked ? Instantiate(m_spawnableComponentPrefab, transform) : Instantiate(m_spawnableComponentPrefab, transform.position, transform.rotation);
        }
    }

}
