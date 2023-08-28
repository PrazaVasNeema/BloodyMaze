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
        [SerializeField] private TMP_FontAsset[] m_fonts;
        [SerializeField] private ScrollRect m_scrollRect;
        [SerializeField] private GameObject m_item;

        private TMP_FontAsset m_defaultFont;
        private float m_defaultFontSize;
        private float m_defaultLineSpacing;
        private float m_defaultRectPosYOffset;

        private void Awake()
        {
            m_noteImage.sprite = m_defaultBackground;
            m_defaultFont = m_noteText.font;
            m_defaultFontSize = m_noteText.fontSize;
            m_defaultLineSpacing = m_noteText.lineSpacing;
            m_defaultRectPosYOffset = m_noteText.GetComponent<RectTransform>().position.y;
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
            LocNotesText noteData = GameController.instance.locData.GetNoteData(key);
            m_noteText.font = noteData.fontNum < m_fonts.Length && noteData.fontNum >= 0 ? m_fonts[noteData.fontNum] : m_defaultFont;
            m_noteText.fontSize = noteData.fontSize != 0f ? noteData.fontSize : m_defaultFontSize;
            m_noteText.lineSpacing = noteData.spacingLine != 0f ? noteData.spacingLine : m_defaultLineSpacing;
            m_noteText.GetComponent<RectTransform>().localPosition = new Vector2(m_noteText.GetComponent<RectTransform>().localPosition.x, noteData.posYOffset != 0 ? noteData.posYOffset : m_defaultRectPosYOffset);
            m_noteText.text = GameController.instance.locData.GetNoteText(key);
            if (background)
                m_noteImage.sprite = background;
            Canvas.ForceUpdateCanvases();

            // item.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            //m_item.GetComponent<ContentSizeFitter>().SetLayoutVertical();

           // m_scrollRect.content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            m_scrollRect.content.GetComponent<ContentSizeFitter>().SetLayoutVertical();

            m_scrollRect.verticalNormalizedPosition = 1;
        }

    }
}
