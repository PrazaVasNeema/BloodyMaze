using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BloodyMaze.UI
{
    public class UINote : MonoBehaviour
    {
        [SerializeField] private Image m_noteImage;
        [SerializeField] private TMP_Text m_noteText;
        [SerializeField] private Sprite m_defaultBackground;

        private void Awake()
        {
            m_noteImage.sprite = m_defaultBackground;
        }

        private void OnEnable()
        {
            GameEvents.OnShowNote += FillNote;
        }

        private void OnDisable()
        {
            m_noteImage.sprite = m_defaultBackground;
            GameEvents.OnShowNote -= FillNote;
        }

        private void FillNote(string key, Sprite background)
        {
            m_noteText.text = GameController.locData.GetNoteText(key);
            if (background)
                m_noteImage.sprite = background;
        }

    }
}
