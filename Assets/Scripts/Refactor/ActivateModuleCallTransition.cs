using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Controllers;
using UnityEditor;

namespace BloodyMaze.Components
{
    public class ActivateModuleCallTransition : ActivateModuleAbstract
    {

        [HideInInspector]
        public int RoomChoiceIndex = 0;
        [HideInInspector]
        // public List<string> room = new List<string>(new string[] { "ChapterRavenWingRoomSafe_zone", "ChapterRavenWingRoomOutsides" });
        public List<string> room = new List<string>(new string[] { "ChapterRavenWingRoomSafe_zone", "ChapterRavenWingRoomOutsides" });
        [SerializeField] private int m_nextRoomSpawnNum = 0;

        public override void ActivateModule()
        {
            // TODO сам транзишн
            // var activateOnInteract = GetComponent<ActivateOnInteract>();
            // if (activateOnInteract)
            //     activateOnInteract.interactComponent.OnInteract -= activateOnInteract.Activate;
            // GameEvents.OnCallGotoFunction?.Invoke("none");
            LevelControllerRe.current.LoadRoomScene(room[RoomChoiceIndex], m_nextRoomSpawnNum);
        }
    }

    [CustomEditor(typeof(ActivateModuleCallTransition))]
    public class DropdownEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //do this first to make sure you have the latest version
            serializedObject.Update();
            ActivateModuleCallTransition script = (ActivateModuleCallTransition)target;

            SerializedProperty specialProp = serializedObject.FindProperty("RoomChoiceIndex");
            specialProp.intValue = EditorGUILayout.Popup("NextRoomSceneName", script.RoomChoiceIndex, script.room.ToArray());
            // script.RoomChoiceIndex = EditorGUILayout.Popup(script.RoomChoiceIndex, script.room.ToArray());
            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }
    }
}