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
        public static System.Action OnHideNote;

        public static System.Action<string> OnUIGMessagesChangeState;
    }
}
