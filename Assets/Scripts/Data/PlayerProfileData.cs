using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [System.Serializable]
    public class PlayerProfileData
    {
        public CharacterSaveData character = new();
    }

    [System.Serializable]
    public class CharacterSaveData
    {
        public float currentHealth;
        public float maxHealth;
        public float currentMana;
        public float maxMana;
        public float manaRestoringRate;
        public float moveSpeed;

        public AmmoType holyAmmoType;
        public AmmoType silverAmmoType;
    }
}
