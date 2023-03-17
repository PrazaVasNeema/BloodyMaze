using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(fileName = "LocDataSO", menuName = "LocDataSO")]
    [System.Serializable]
    public class LocDataSO : ScriptableObject
    {
        public List<LocNotesText> locNotesTexts;
        public List<LocDialogueData> locDialogueTexts;
        public List<LocInterfaceText> locInterfaceTexts;

        public string GetNoteText(string key)
        {
            return locNotesTexts.Find((x) => x.key == key).text[GameController.instance.playerProfileSO.playerProfileData.optionsData.language];
            // var temp = locNotesTexts.Find((x) => x.key == key);
            // return locNotesTexts.Find((x) => x.key == key).text[0];
            // return locNotesTexts[0].text[0];
        }

        public Dialogue GetDialogue(string key)
        {
            return locDialogueTexts.Find((x) => x.key == key).dialogue[GameController.instance.playerProfileSO.playerProfileData.optionsData.language];

        }
    }


    [System.Serializable]
    public class LocNotesText
    {
        public string key;
        public string[] text;
    }

    [System.Serializable]
    public class LocDialogueData
    {
        public string key;
        public Dialogue[] dialogue;
    }

    [System.Serializable]
    public class LocInterfaceText
    {
        public string key;
        public string[] text;
    }
}
