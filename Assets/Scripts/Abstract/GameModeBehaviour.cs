using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace BloodyMaze.States
{
    public abstract class GameModeBehavior : MonoBehaviour
    {
        [SerializeField] private List<LocalizableTextField> m_localizableTextFields;
        [SerializeField] private List<GameStateBehavior> m_states;
        private TMP_Text[] m_interfaceTextFields;
        private StateMachine m_stateMachine;

        private void Awake()
        {
            InitGameController();
            InitStates();
            m_interfaceTextFields = Resources.FindObjectsOfTypeAll(typeof(TMP_Text)) as TMP_Text[];
            InitInterfaceLocalization();
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
        protected void InitInterfaceLocalization()
        {
            foreach (TMP_Text textField in m_interfaceTextFields)
            {
                if (textField.text.Contains("UILoc_"))
                {
                    Debug.Log("InitInterfaceLocalization");
                    textField.text = GameController.instance.locData.GetInterfaceText(textField.text);
                }
            }
        }
    }
}