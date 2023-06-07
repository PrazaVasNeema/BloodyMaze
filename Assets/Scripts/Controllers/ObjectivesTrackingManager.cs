using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.UI;

namespace BloodyMaze.Controllers
{
    public class ObjectivesTrackingManager : MonoBehaviour
    {
        private UIJournal m_uiJournal;

        public void Init(UIJournal uIJournal)
        {
            m_uiJournal = uIJournal;
            FillFields();
        }

        private void FillFields()
        {
            Debug.Log("FillFields");
            List<GlobalEventsData> m_globalEventsData = GameController.instance.playerProfile.playerProfileData.globalEventsData;
            bool endReached = false, endFilled = false;
            foreach (GlobalEventsData data in m_globalEventsData)
            {
                if (endReached && endFilled)
                    break;
                else if (!data.flag)
                {
                    endReached = true;
                    endFilled = false;
                }
                if (!string.IsNullOrEmpty(data.objectiveText))
                {
                    m_uiJournal.UpdateObjectives(data.objectiveText);
                    endFilled = true;
                }
            }
        }
    }
}
