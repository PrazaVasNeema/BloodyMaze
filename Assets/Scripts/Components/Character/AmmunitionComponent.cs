using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BloodyMaze.Components
{

    public class AmmunitionComponent : MonoBehaviour
    {
        Dictionary<string, AmmoType> m_ammoType = new Dictionary<string, AmmoType>();

        public System.Action<string, AmmoType> onAmmoCountChange;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            m_ammoType.Add("holy", new AmmoType(36, 36, 6, 6));
            m_ammoType.Add("silver", new AmmoType(36, 36, 6, 6));
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
