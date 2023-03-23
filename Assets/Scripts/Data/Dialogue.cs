using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [System.Serializable]
    public class Dialogue
    {
        public List<SentenceData> sentencesData = new();
    }

    [System.Serializable]
    public class SentenceData
    {
        public string personName;
        [TextArea(3, 10)]
        public string sentence;
    }
}
