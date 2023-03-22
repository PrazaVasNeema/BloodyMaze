using UnityEngine;
using System.Collections.Generic;
using Unity.UI;
using TMPro;

namespace BloodyMaze.States
{
    public class SelectLevelState : GameStateBehavior
    {
        [SerializeField] private UILevelInfo m_uiLevelInfo;
        [SerializeField] private GameObject m_saveSlotsLayoutGroup;

        private PlayerProfileSO m_playerProfile;
        private LevelsInfoSO m_levelsInfo;

        private IReadOnlyList<LevelsInfoSO.Data> m_levelsData;

        private void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerProfileData playerProfileData = GameController.allPlayerProfilesData[i];
                int progressPercent = playerProfileData.globalEventsData.Count;
                int totalEventsTrue = 0;
                playerProfileData.globalEventsData.ForEach(x => totalEventsTrue += x.flag == true ? 1 : 0);
                m_saveSlotsLayoutGroup.transform.GetChild(i).GetChild(2).GetComponent<TMP_Text>().text = playerProfileData
                == null ? "Пустой слот" : $"Прогресс: {Mathf.Round(totalEventsTrue * 1f / (progressPercent * 1f) * 100f * 10f)}";
            }
        }

        protected override void OnEnter()
        {
            m_playerProfile = GameController.playerProfile;
            m_levelsInfo = GameController.levelsInfo;
            m_levelsData = m_levelsInfo.GetLevels();
            if (!TrySelectLevel(m_playerProfile.playerProfileData.characterSaveData.lastLevelIndex))
            {
                TrySelectLevel(0);
            }

        }

        public void NextLevel()
        {
            if (m_playerProfile.playerProfileData.characterSaveData.lastLevelIndex < m_levelsData.Count - 1)
            {
                TrySelectLevel(m_playerProfile.playerProfileData.characterSaveData.lastLevelIndex + 1);
            }
        }

        public void PrevLevel()
        {
            if (m_playerProfile.playerProfileData.characterSaveData.lastLevelIndex > 0)
            {
                TrySelectLevel(m_playerProfile.playerProfileData.characterSaveData.lastLevelIndex - 1);
            }
        }

        private bool TrySelectLevel(int index)
        {
            if (index >= 0 && index < m_levelsData.Count)
            {
                m_uiLevelInfo.SetInfo(m_levelsData[index]);
                m_playerProfile.playerProfileData.characterSaveData.lastLevelIndex = index;
                return true;
            }
            return false;
        }
    }
}