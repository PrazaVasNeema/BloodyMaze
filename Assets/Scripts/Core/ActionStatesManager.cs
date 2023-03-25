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
            bool isBattleState = false;
            switch (current.m_state)
            {
                case ActionStates.EXPLORING:
                    if (current.m_enemiesTriggered > 0)
                    {
                        current.m_state = ActionStates.BATTLE;
                        GameEvents.OnHideMessage?.Invoke();
                        isBattleState = true;
                    }
                    else
                    {
                        current.m_state = ActionStates.INTERACTING;
                        GameEvents.OnEnterInteractingState?.Invoke();
                    }
                    break;
                case ActionStates.BATTLE:
                    if (current.m_enemiesTriggered == 0)
                        current.m_state = ActionStates.EXPLORING;
                    break;
                case ActionStates.INTERACTING:
                    current.m_state = ActionStates.EXPLORING;
                    break;
            }
            GameEvents.OnBattleActionStateIsSet?.Invoke(isBattleState);
        }

        public static void SetState(ActionStates stateToSet)
        {
            current.m_state = stateToSet;
            if (current.m_state == ActionStates.INTERACTING)
                GameEvents.OnEnterInteractingState?.Invoke();
        }

        public static void ChangeEnemiesTriggeredCount(int changingDir)
        {
            current.m_enemiesTriggered += changingDir;
            current.m_enemiesTriggered = current.m_enemiesTriggered < 0 ? 0 : current.m_enemiesTriggered;
        }
    }
}
