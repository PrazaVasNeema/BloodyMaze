using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [CreateAssetMenu(fileName = "LocDataSO", menuName = "BloodyMaze/LocDataSO")]
    [System.Serializable]
    public class LocDataSO : ScriptableObject
    {
        public List<LocNotesText> locNotesTexts;
        public List<LocDialogueData> locDialogueTexts;
        public List<LocInterfaceText> locInterfaceTexts;
        public List<LocMessagesText> locMessagesText;
        public List<LocMiniMessagesText> locMiniMessagesText;
        public List<LocJournalNotesText> locJournalNotesText;
        public List<PersonData> locPersonsData;

        public List<List<string>> GetPeopleNamesSortByLivingStatus()
        {
            List<List<string>> peopleNamesByLivingStatus = new();
            peopleNamesByLivingStatus.Add(new List<string>());
            peopleNamesByLivingStatus.Add(new List<string>());
            foreach (PersonData personData in locPersonsData)
            {
                GlobalEventsData globalEvent = GameController.instance.playerProfile.playerProfileData.globalEventsData.Find((x) => x.eventKey == personData.correspondingEventKey);
                if (globalEvent != null)
                {
                    switch (globalEvent.flag)
                    {
                        case true:
                            peopleNamesByLivingStatus[1].Add(personData.name);
                            break;
                        case false:
                            peopleNamesByLivingStatus[0].Add(personData.name);
                            break;
                    }
                }
            }
            return peopleNamesByLivingStatus;
            // var temp = locNotesTexts.Find((x) => x.key == key);
            // return locNotesTexts.Find((x) => x.key == key).text[0];
            // return locNotesTexts[0].text[0];
        }

        public string GetNoteText(string key)
        {
            return locNotesTexts.Find((x) => x.key == key).text[GameController.instance.gameOptions.GameOptionsData.language];
            // var temp = locNotesTexts.Find((x) => x.key == key);
            // return locNotesTexts.Find((x) => x.key == key).text[0];
            // return locNotesTexts[0].text[0];
        }
        public LocNotesText GetNoteData(string key)
        {
            return locNotesTexts.Find((x) => x.key == key);
            // var temp = locNotesTexts.Find((x) => x.key == key);
            // return locNotesTexts.Find((x) => x.key == key).text[0];
            // return locNotesTexts[0].text[0];
        }

        public Dialogue GetDialogue(string key)
        {
            return locDialogueTexts.Find((x) => x.key == key).dialogue[GameController.instance.gameOptions.GameOptionsData.language];

        }

        // . . .

        public string GetMessage(string key)
        {
            return locMessagesText.Find((x) => x.key == key).text[GameController.instance.gameOptions.GameOptionsData.language];

        }

        public string GetMiniMessage(string key)
        {
            return locMiniMessagesText.Find((x) => x.key == key).text[GameController.instance.gameOptions.GameOptionsData.language];

        }

        public string GetInterfaceText(string key)
        {
            var requiredLocInterfaceText = locInterfaceTexts.Find((x) => x.key == key);
            return requiredLocInterfaceText == null ? key : requiredLocInterfaceText.text[GameController.instance.gameOptions.GameOptionsData.language];

        }
    }


    [System.Serializable]
    public class LocNotesText
    {
        public string key;
        public int fontNum;
        public float fontSize;
        public float spacingLine;
        public float posYOffset;
        [TextArea(3, 12)]
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

    [System.Serializable]
    public class LocMessagesText
    {
        public string key;
        public string[] text;
    }

    [System.Serializable]
    public class LocJournalNotesText
    {
        public string key;
        public string[] text;
    }

    [System.Serializable]
    public class LocMiniMessagesText
    {
        public string key;
        public string[] text;
    }

    [System.Serializable]
    public class PersonData
    {
        public string correspondingEventKey;
        public string name;
    }
}
