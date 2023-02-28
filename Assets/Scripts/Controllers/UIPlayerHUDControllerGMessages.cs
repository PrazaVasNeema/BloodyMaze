using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BloodyMaze
{
    public class UIPlayerHUDControllerGMessages : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_messageTMPText;
        private GameObject m_messageTMPTextParent;
        [SerializeField] private TMP_Text m_noteTMPText;
        private GameObject m_noteTMPTextParent;

        private enum UIGMessagesState { NONE, MESSAGE, NOTE };
        private UIGMessagesState m_state = UIGMessagesState.NONE;

        private void Awake()
        {
            m_messageTMPTextParent = m_messageTMPText.transform.parent.transform.parent.gameObject;
            m_messageTMPTextParent.SetActive(false);

            m_noteTMPTextParent = m_noteTMPText.transform.parent.transform.parent.gameObject;
            m_noteTMPTextParent.SetActive(false);

            GameEvents.OnUIGMessagesChangeState += ChangeState;
        }

        private void OnDestroy()
        {
            GameEvents.OnUIGMessagesChangeState -= ChangeState;
        }

        private void ChangeState(string text)
        {
            switch (m_state)
            {
                case UIGMessagesState.NONE:
                    m_state = UIGMessagesState.MESSAGE;
                    ShowMessage(text);
                    break;
                case UIGMessagesState.MESSAGE:
                    if (text != null)
                    {
                        HideMessage();
                        ShowNote(text);
                        m_state = UIGMessagesState.NOTE;
                    }
                    else
                    {
                        HideMessage();
                        m_state = UIGMessagesState.NONE;
                    }
                    break;
                case UIGMessagesState.NOTE:
                    HideNote();
                    m_state = UIGMessagesState.NONE;
                    break;
            }
        }

        private void ShowMessage(string messageText)
        {
            m_messageTMPText.SetText(messageText);
            m_messageTMPTextParent.SetActive(true);
        }

        private void HideMessage()
        {
            m_messageTMPTextParent.SetActive(false);
        }

        private void ShowNote(string noteText)
        {
            HideMessage();
            m_noteTMPText.SetText(noteText);
            m_noteTMPTextParent.SetActive(true);
        }

        private void HideNote()
        {
            m_noteTMPTextParent.SetActive(false);
        }

    }
}
