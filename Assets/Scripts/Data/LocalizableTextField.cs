using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BloodyMaze
{
    [System.Serializable]
    public class LocalizableTextField
    {
        public TMP_Text textField;
        public string interfaceLocKey;

        public void LocalizeTextField()
        {
            textField.text = GameController.instance.locData.GetInterfaceText(interfaceLocKey);
        }
    }
}
