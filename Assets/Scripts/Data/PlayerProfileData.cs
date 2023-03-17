using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [System.Serializable]
    public class PlayerProfileData
    {
        public CharacterSaveData characterSaveData = new();
        public List<GlobalEventsData> globalEventsData = new();
        public OptionsData optionsData = new();
        public List<RoomsData> roomsData = new();

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

    [System.Serializable]
    public class GlobalEventsData
    {
        public string desc;
        public bool flag;
    }

    [System.Serializable]
    public class OptionsData
    {
        public int language;
    }

    [System.Serializable]
    public class RoomsData
    {
        public List<int> enemiesToSpawnIDs = new();
        public List<int> itemsToSpawnIDs = new();
    }
}
