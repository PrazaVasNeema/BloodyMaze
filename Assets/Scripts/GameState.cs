using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public enum GameStates { EXPLORING, BATTLE, INTERACTING };

    public class GameState : MonoBehaviour
    {
        public static GameState current { private set; get; }

        private GameStates m_state = GameStates.EXPLORING;
        public GameStates state => m_state;
        private float m_enemiesTriggered = 0;

        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        public void ChangeState()
        {
            // Debug.Log("ChangeState");
            switch (m_state)
            {
                case GameStates.EXPLORING:
                    if (m_enemiesTriggered > 0)
                        m_state = GameStates.BATTLE;
                    else
                        m_state = GameStates.INTERACTING;
                    break;
                case GameStates.BATTLE:
                    if (m_enemiesTriggered == 0)
                        m_state = GameStates.EXPLORING;
                    break;
                case GameStates.INTERACTING:
                    m_state = GameStates.EXPLORING;
                    break;
            }
        }

        public void SetState(GameStates stateToSet)
        {
            m_state = stateToSet;
        }

        public void ChangeEnemiesTriggeredCount(int changingDir)
        {
            m_enemiesTriggered += changingDir;
        }
    }
}
