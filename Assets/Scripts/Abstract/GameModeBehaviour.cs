using System;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze.States
{
    public abstract class GameModeBehavior : MonoBehaviour
    {
        [SerializeField] private List<GameStateBehavior> m_states;
        private StateMachine m_stateMachine;

        private void Awake()
        {
            Debug.Log("1");
            InitGameController();
            Debug.Log("2");
            InitStates();
            Debug.Log("3");
        }

        private void InitStates()
        {
            m_stateMachine = new StateMachine(m_states);
            m_states.ForEach(InitGameState);
        }

        private void Start()
        {
            m_stateMachine.Change(m_states[0].GetType());
        }

        private void InitGameState(GameStateBehavior state)
        {
            foreach (var view in state.GetViews())
            {
                view.SetActive(false);
            }

            state.gameObject.SetActive(false);
        }

        protected void ChangeState<T>() where T : IGameState
        {
            m_stateMachine.Change(typeof(T));
        }

        private static void InitGameController()
        {
            Debug.Log("4");
#if UNITY_EDITOR
            if (GameController.instance == null)
            {
                var assets = UnityEditor.AssetDatabase.FindAssets("GameController");
                foreach (var guid in assets)
                {
                    var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                    var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (prefab)
                    {
                        Instantiate(prefab);
                        break;
                    }
                }
            }
#endif
        }
    }
}