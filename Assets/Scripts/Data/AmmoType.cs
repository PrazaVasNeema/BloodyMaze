using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [System.Serializable]
    public class AmmoType
    {
        public int maxAmmo = 0;
        public int currentAmmo = 0;
        public int roundSize = 0;
        public int currentRoundAmmo = 0;


        // public AmmoType(int maxAmmo, int currentAmmo, int roundSize, int currentRoundAmmo)
        // {
        //     this.maxAmmo = maxAmmo;
        //     this.currentAmmo = currentAmmo;
        //     this.roundSize = roundSize;
        //     this.currentRoundAmmo = currentRoundAmmo;
        // }

        public AmmoType Clone() => new AmmoType
        {

            maxAmmo = this.maxAmmo,
            currentAmmo = this.currentAmmo,
            roundSize = this.roundSize,
            currentRoundAmmo = this.currentRoundAmmo
        };

        public bool ShootAmmo()
        {
            if (currentRoundAmmo != 0)
            {
                currentRoundAmmo--;
                return true;
            }
            return false;
        }

        public bool Reload()
        {
            if (currentAmmo > 0 && currentRoundAmmo != roundSize)
            {
                currentAmmo -= roundSize - currentRoundAmmo;
                currentRoundAmmo = roundSize;
                return true;
            }
            return false;
        }

        public bool AddAmmo()
        {
            currentAmmo = maxAmmo;
            return true;
        }


    }

    public class Level
    {
        public string level_name = "";
        public int ID = 0;
        public int IDd = 0;


        public Level Clone() => new Level
        {
            level_name = this.level_name,
            ID = this.ID
        };
    }
}
