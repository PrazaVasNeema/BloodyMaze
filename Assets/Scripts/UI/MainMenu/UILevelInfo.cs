using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BloodyMaze
{
    public class UILevelInfo : MonoBehaviour
    {
        [SerializeField] private GameObject m_selectLevelPanel;
        [SerializeField] private GameObject m_chooseDestinyPanel;
        public TMPro.TextMeshProUGUI levelNameText;
        public Image previewImage;

        public void SetInfo(LevelsInfoSO.Data data)
        {
            levelNameText.text = data.name;
            previewImage.sprite = data.icon;
        }

        public void GotoChooseDestiny()
        {
            m_selectLevelPanel.SetActive(false);
            m_chooseDestinyPanel.SetActive(true);
        }

        public void GotoSelectLevel()
        {
            m_selectLevelPanel.SetActive(true);
            m_chooseDestinyPanel.SetActive(false);
        }
    }
}
