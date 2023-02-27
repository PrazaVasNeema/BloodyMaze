using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(menuName = "BloodyMaze/CharacterData", fileName = "CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public float currentHealth = 100f;
        public float maxHealth = 100f;
        public float currentMana = 100f;
        public float maxMana = 100f;
        public float manaRestoringRate = 10f;
        public float moveSpeed = 100f;
        public int currentAmmoAmountHoly = 42;
        public int currentAmmoAmountSilver = 42;
    }
}
