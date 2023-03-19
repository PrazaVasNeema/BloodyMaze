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
        /// <summary>
        /// States: gameplay, note, dialogue,
        /// journal, none, reload_level,
        /// main_menu
        /// </summary>
        /// <param name="stateName"></param>
        public static System.Action<string> OnCallGotoFunction;
        public static System.Action OnStateChanged;
        public static System.Action OnTransition;
        public static System.Action<string> OnEventFlagCheck;
        public static System.Action OnPCDeath;
        public static System.Action<string> OnShowMiniMessage;
        public static System.Action OnInitLevelComplete;
        public static System.Action OnEnterGameplayState;
    }
}
