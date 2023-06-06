using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BloodyMaze.UI
{
    public class UIMessage : MonoBehaviour
    {
        private TMP_Text m_messageTMPText;
        private GameObject m_subLevel;

        private void Awake()
        {
            m_subLevel = transform.GetChild(0).gameObject;
            m_messageTMPText = GetComponentInChildren<TMP_Text>();
            HideMessage();
        }

        private void OnEnable()
        {
            GameEvents.OnShowMessage += ShowMessage;
            GameEvents.OnHideMessage += HideMessage;
            HideMessage();
        }

        private void OnDisable()
        {
            GameEvents.OnShowMessage -= ShowMessage;
            GameEvents.OnHideMessage -= HideMessage;
        }

        private void ShowMessage(string key)
        {
            m_subLevel.SetActive(true);

            m_messageTMPText.SetText(GameController.instance.locData.GetMessage(key));
        }

        private void HideMessage()
        {
            m_subLevel.SetActive(false);
        }
    }
}
