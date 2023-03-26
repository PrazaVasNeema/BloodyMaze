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
        public bool m_isRealoading;


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
            if (!m_isRealoading)
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
                GameEvents.OnReload?.Invoke();
                m_isRealoading = true;
                OnReload?.Invoke();
                StartCoroutine(ReloadCo());
                Debug.Log("Reload");
                return true;
            }
            return false;
        }

        public void AddAmmo()
        {
            m_ammoType["holy"].AddAmmo();
            m_ammoType["holy"].Reload();
            onAmmoCountChange?.Invoke("holy", m_ammoType["holy"]);
        }

        IEnumerator ReloadCo()
        {
            yield return new WaitForSeconds(1f);
            m_isRealoading = false;
            onAmmoCountChange?.Invoke("holy", m_ammoType["holy"]);
        }
    }

}
