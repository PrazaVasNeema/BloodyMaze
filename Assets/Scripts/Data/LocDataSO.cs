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
        public List<LocDialogueText> locDialogueTexts;
        public List<LocInterfaceText> locInterfaceTexts;

        public string GetNoteText(string key)
        {
            return locNotesTexts.Find((x) => x.key == key).text[GameController.instance.playerProfileSO.playerProfileData.optionsData.language];
            // var temp = locNotesTexts.Find((x) => x.key == key);
            // return locNotesTexts.Find((x) => x.key == key).text[0];
            // return locNotesTexts[0].text[0];
        }


    }


    [System.Serializable]
    public class LocNotesText
    {
        public string key;
        public string[] text;
    }

    [System.Serializable]
    public class LocDialogueText
    {
        public string key;
        public Dialogue[] text;
    }

    [System.Serializable]
    public class LocInterfaceText
    {
        public string key;
        public string[] text;
    }
}
