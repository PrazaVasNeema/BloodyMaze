using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [System.Serializable]
    public class MedsType
    {
        public string name;
        public int hpToHeal;
        public int maxAmount;
        public int currentAmount;

        public bool UseMeds()
        {
            if (currentAmount != 0)
            {
                currentAmount--;
                return true;
            }
            return false;
        }

        public bool AddMeds()
        {
            if (currentAmount != maxAmount)
            {
                currentAmount++;
                return true;
            }
            return false;
        }

        public bool FullUpMeds()
        {
            currentAmount = maxAmount;
            return true;
        }
    }
}
