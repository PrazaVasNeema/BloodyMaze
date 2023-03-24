using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BloodyMaze.Components
{

    public class AmmunitionComponent : MonoBehaviour
    {
        public Dictionary<string, AmmoType> m_ammoType = new Dictionary<string, AmmoType>();

        public System.Action<string, AmmoType> onAmmoCountChange;
        public UnityEvent OnReload;


        public void Init(AmmoType holyAmmoType)
        {
            m_ammoType.Add("holy", holyAmmoType.Clone());
            // m_ammoType["holy"].Reload();
            // m_ammoType["silver"].Reload();
            // onAmmoCountChange?.Invoke("holy", m_ammoType["holy"]);

            // onAmmoCountChange?.Invoke("silver", m_ammoType["silver"]);

        }

        public bool ShootAmmo(string ammoTypeName)
        {
            if (m_ammoType[ammoTypeName].ShootAmmo())
            {
                onAmmoCountChange?.Invoke(ammoTypeName, m_ammoType[ammoTypeName]);
                return true;
            }
            return false;
        }

        public bool Reload(string ammoTypeName)
        {
            if (m_ammoType[ammoTypeName].Reload())
            {
                Debug.Log("Reload");
                onAmmoCountChange?.Invoke(ammoTypeName, m_ammoType[ammoTypeName]);
                OnReload?.Invoke();
                return true;
            }
            return false;
        }

    }

}
