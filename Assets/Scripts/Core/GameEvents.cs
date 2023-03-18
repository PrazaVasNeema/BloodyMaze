using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public static class GameEvents
    {
        public static System.Action<string> OnShowMessage;
        public static System.Action OnHideMessage;
        public static System.Action<string> OnShowNote;
        public static System.Action<string, string> OnStartDialogue;
        public static System.Action OnSaveData;
        public static System.Action<int> OnChangeGameplayState;
        public static System.Action OnStateChanged;
        public static System.Action OnTransition;
        public static System.Action<string> OnEventFlagChecked;
        public static System.Action OnPCDeath;
    }
}
