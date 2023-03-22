using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BloodyMaze.UI
{
    public class UIChooseSaveFile : MonoBehaviour
    {
        [SerializeField] private GameObject m_saveSlotsLayoutGroup;

        private void OnEnable()
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerProfileData playerProfileData = GameController.allPlayerProfilesData[i];
                int progressPercent = new();
                int totalEventsTrue = 0;
                if (playerProfileData != null)
                {
                    progressPercent = playerProfileData == null ? 0 : playerProfileData.globalEventsData.Count;
                    playerProfileData.globalEventsData.ForEach(x => totalEventsTrue += x.flag == true ? 1 : 0);
                }
                m_saveSlotsLayoutGroup.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = playerProfileData
                == null ? "Пустой слот" : $"Прогресс: {Mathf.Round(totalEventsTrue * 1f / (progressPercent * 1f) * 100f * 1f)}%";
            }
        }
    }
}
