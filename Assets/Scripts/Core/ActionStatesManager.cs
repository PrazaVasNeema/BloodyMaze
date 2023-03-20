using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public enum ActionStates { EXPLORING, BATTLE, INTERACTING };

    public class ActionStatesManager : MonoBehaviour
    {
        public static ActionStatesManager current { private set; get; }

        private ActionStates m_state = ActionStates.EXPLORING;
        public static ActionStates state => current.m_state;
        private float m_enemiesTriggered = 0;

        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        public static void ChangeState()
        {
            switch (current.m_state)
            {
                case ActionStates.EXPLORING:
                    if (current.m_enemiesTriggered > 0)
                    {
                        current.m_state = ActionStates.BATTLE;
                        GameEvents.OnHideMessage?.Invoke();
                    }
                    else
                        current.m_state = ActionStates.INTERACTING;
                    break;
                case ActionStates.BATTLE:
                    if (current.m_enemiesTriggered == 0)
                        current.m_state = ActionStates.EXPLORING;
                    break;
                case ActionStates.INTERACTING:
                    current.m_state = ActionStates.EXPLORING;
                    break;
            }
        }

        public static void SetState(ActionStates stateToSet)
        {
            current.m_state = stateToSet;
        }

        public static void ChangeEnemiesTriggeredCount(int changingDir)
        {
            current.m_enemiesTriggered += changingDir;
        }
    }
}
