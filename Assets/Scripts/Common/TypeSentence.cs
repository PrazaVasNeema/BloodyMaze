using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BloodyMaze
{
    public static class TypeSentence
    {
        public static IEnumerator TypeSentenceStatic(TMP_Text fieldToFill, string text, AudioSource audioSourceToPlay, float typingSpeed)
        {
            fieldToFill.text = "";
            foreach (char letter in text.ToCharArray())
            {
                fieldToFill.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
}
