using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public class MedsComponent : MonoBehaviour
    {
        public System.Action<MedsType> OnMedsCountChange;


        private MedsType m_meds;
        public MedsType meds => m_meds;

        private void OnEnable()
        {
            GameEvents.OnInitLevelComplete += CallChange;
        }

        private void CallChange()
        {
            OnMedsCountChange?.Invoke(m_meds);
            GameEvents.OnInitLevelComplete -= CallChange;
        }

        public void Init(MedsType commonMedsType)
        {
            m_meds = commonMedsType;
            OnMedsCountChange?.Invoke(m_meds);
        }

        public bool UseMeds()
        {
            if (m_meds.UseMeds())
            {
                OnMedsCountChange?.Invoke(m_meds);

                return true;
            }
            return false;
        }

        public bool AddMeds()
        {
            if (m_meds.AddMeds())
            {
                OnMedsCountChange?.Invoke(m_meds);
                return true;
            }
            return false;
        }

        public void FullUpMeds()
        {
            m_meds.FullUpMeds();
            OnMedsCountChange?.Invoke(m_meds);
        }
    }
}
