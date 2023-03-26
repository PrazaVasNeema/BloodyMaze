using UnityEngine;
using System.Collections.Generic;

namespace BloodyMaze.States
{
    public class SelectLevelState : GameStateBehavior
    {
        [SerializeField] private UILevelInfo m_uiLevelInfo;

        private PlayerProfileSO m_playerProfile;
        private LevelsInfoSO m_levelsInfo;

        private IReadOnlyList<LevelsInfoSO.Data> m_levelsData;

        private void Start()
        {

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
                m_uiLevelInfo.SetInfo(m_levelsData[index], index == m_levelsData.Count - 1 ? true : false, index == 0 ? true : false);
                m_playerProfile.playerProfileData.characterSaveData.lastLevelIndex = index;
                return true;
            }
            return false;
        }
    }
}