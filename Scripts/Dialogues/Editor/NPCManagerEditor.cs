using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Scripts.NPC
{
    [CustomEditor(typeof(NPCManager))]
    public class NPCManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            NPCManager manager = target as NPCManager;
            DrawDefaultInspector();
            foreach(KeyValuePair<string, NPC> npc in manager)
            {
                EditorGUILayout.LabelField(npc.Value.name + " " + npc.Value.reputation);
            }
        }
    }
}
