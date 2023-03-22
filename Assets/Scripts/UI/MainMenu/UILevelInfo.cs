using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BloodyMaze
{
    public class UILevelInfo : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI levelNameText;
        public Image previewImage;

        public void SetInfo(LevelsInfoSO.Data data)
        {
            levelNameText.text = data.name;
            previewImage.sprite = data.icon;
        }
    }
}
