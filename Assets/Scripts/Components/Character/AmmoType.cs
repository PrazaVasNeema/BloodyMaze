using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class AmmoType
    {
        public int maxAmmo;
        public int currentAmmo;
        public int roundSize;
        public int currentRoundAmmo;

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

        public AmmoType(int maxAmmo, int currentAmmo, int roundSize, int currentRoundAmmo)
        {
            this.maxAmmo = maxAmmo;
            this.currentAmmo = currentAmmo;
            this.roundSize = roundSize;
            this.currentRoundAmmo = currentRoundAmmo;
        }
    }
}
