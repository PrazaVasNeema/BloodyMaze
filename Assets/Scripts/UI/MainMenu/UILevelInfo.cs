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
        [SerializeField] private GameObject m_buttonNext;
        [SerializeField] private GameObject m_buttonPrev;
        public TMPro.TextMeshProUGUI levelNameText;
        public Image previewImage;

        public void SetInfo(LevelsInfoSO.Data data)
        {
            m_buttonPrev.SetActive(false);
            levelNameText.text = data.name;
            previewImage.sprite = data.icon;
        }

        public void SetInfo(LevelsInfoSO.Data data, bool isLast, bool isFirst)
        {
            m_buttonNext.SetActive(!isLast);
            m_buttonPrev.SetActive(!isFirst);
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
