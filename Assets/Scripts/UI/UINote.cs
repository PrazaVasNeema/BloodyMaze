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

        private void OnEnable()
        {
            GameEvents.OnShowNote += FillNote;
        }

        private void OnDisable()
        {
            GameEvents.OnShowNote -= FillNote;
        }

        private void FillNote(string key)
        {
            m_noteText.text = GameController.instance.locData.GetNoteText(key);
        }

    }
}
