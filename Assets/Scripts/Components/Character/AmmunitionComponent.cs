using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.Components
{

    public class AmmunitionComponent : MonoBehaviour
    {
        Dictionary<string, AmmoType> m_ammoType = new Dictionary<string, AmmoType>();

        public bool ShootAmmo()
        {
            return true;

        }

        public bool Reload()
        {
            return true;
        }

    }

}
