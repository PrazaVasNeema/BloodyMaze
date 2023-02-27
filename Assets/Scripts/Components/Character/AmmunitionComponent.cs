using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BloodyMaze.Components
{

    public class AmmunitionComponent : MonoBehaviour
    {
        Dictionary<string, AmmoType> m_ammoType = new Dictionary<string, AmmoType>();

        public System.Action<string, AmmoType> onAmmoCountChange;

        public void Init(int currentAmmoAmountHoly, int currentAmmoAmountSilver)
        {
            m_ammoType.Add("holy", new AmmoType(42, currentAmmoAmountHoly, 6, 0));
            m_ammoType.Add("silver", new AmmoType(42, currentAmmoAmountSilver, 6, 0));
            // m_ammoType["holy"].Reload();
            // m_ammoType["silver"].Reload();
            // onAmmoCountChange?.Invoke("holy", m_ammoType["holy"]);

            // onAmmoCountChange?.Invoke("silver", m_ammoType["silver"]);

        }

        public bool ShootAmmo(string ammoTypeName)
        {
            if (m_ammoType[ammoTypeName].ShootAmmo())
            {
                onAmmoCountChange.Invoke(ammoTypeName, m_ammoType[ammoTypeName]);
                return true;
            }
            return false;
        }

        public bool Reload(string ammoTypeName)
        {
            if (m_ammoType[ammoTypeName].Reload())
            {
                onAmmoCountChange.Invoke(ammoTypeName, m_ammoType[ammoTypeName]);
                return true;
            }
            return false;
        }

    }

}
