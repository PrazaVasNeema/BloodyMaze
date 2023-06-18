using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace BloodyMaze.UI
{
    public class UIChooseSaveFile : MonoBehaviour
    {
        [SerializeField] private GameObject m_saveSlotsLayoutGroup;

        private void OnEnable()
        {
            for (int i = 0; i < 3; i++)
            {
                TMP_Text curSaveSlotText = m_saveSlotsLayoutGroup.transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>();
                if (!curSaveSlotText.text.Contains((i + 1).ToString()))
                    curSaveSlotText.text += $" {i + 1}";
                PlayerProfileData playerProfileData = GameController.instance.allPlayerProfilesData[i];
                int progressPercent = new();
                int totalEventsTrue = 0;
                if (playerProfileData != null)
                {
                    progressPercent = playerProfileData == null ? 0 : playerProfileData.globalEventsData.Count;
                    playerProfileData.globalEventsData.ForEach(x => totalEventsTrue += x.flag == true ? 1 : 0);
                }
                m_saveSlotsLayoutGroup.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = playerProfileData
                == null ? GameController.instance.locData.GetInterfaceText("UILoc_save_file_progress_empty") : $"{GameController.instance.locData.GetInterfaceText("UILoc_save_file_progress")}: {Mathf.Round(totalEventsTrue * 1f / (progressPercent * 1f) * 100f * 1f)}%";
                if (GameController.instance.shouldStartNewGame == false && playerProfileData == null)
                    m_saveSlotsLayoutGroup.transform.GetChild(i).GetComponent<Button>().interactable = false;
                else
                    m_saveSlotsLayoutGroup.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
        }
    }
}
