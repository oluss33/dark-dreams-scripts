/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace Scripts.Dialogues
{
    [CustomEditor(typeof(DialogueActivator))]
    public class DialogueActivatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DialogueActivator activator = target as DialogueActivator;
            DrawDefaultInspector();
            SerializedProperty dialogue = serializedObject.FindProperty(nameof(DialogueActivator.dialogue));
            Debug.Log(dialogue);
           // SerializedProperty polishDialogue = dialogue.FindPropertyRelative(nameof(Dialogue.polishMessage));
            Debug.Log(dialogue.propertyPath);
            Debug.Log(dialogue.NextVisible(true));
            Debug.Log(dialogue.propertyType);
            Debug.Log(dialogue.type);
            Debug.Log(dialogue.propertyPath);
            Debug.Log(dialogue.NextVisible(false));
            Debug.Log(dialogue.propertyPath);
          //  Debug.Log(polishDialogue);
          //  DrawSelectedEvent(polishDialogue);
            serializedObject.ApplyModifiedProperties();
        }
        public void DrawSelectedEvent(SerializedProperty message)
        {
            SerializedProperty messageProperty = message.FindPropertyRelative(nameof(Message.messageEvent));
            EditorGUILayout.PropertyField(messageProperty, new GUIContent("Event"));
            SerializedProperty optionArray = message.FindPropertyRelative(nameof(Message.optionArray));
            for(int i = 0; i < optionArray.arraySize; i++)
            {
                SerializedProperty option = optionArray.GetArrayElementAtIndex(i);
                SerializedProperty nextMessage = option.FindPropertyRelative(nameof(DialogueOption.nextMessage));
                EditorGUI.indentLevel += 2;
                DrawSelectedEvent(nextMessage);
                EditorGUI.indentLevel -= 2;
            }
        }
    }
}
*/