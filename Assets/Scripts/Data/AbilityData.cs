using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;

namespace BloodyMaze
{
    [CreateAssetMenu(menuName = "BloodyMaze/AbilityData", fileName = "AbilityData")]
    public class AbilityData : ScriptableObject
    {
        public UseAbilityComponent prefab;
    }
}
