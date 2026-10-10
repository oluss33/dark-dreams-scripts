using UnityEditor;
using UnityEngine;


namespace Scripts.Inventory
{
    //[CustomEditor(typeof(ItemDataCreator))]
    public class ItemDataHolderEditor : Editor
    {
        private SerializedProperty spriteProp;
        void OnEnable()
        {
            spriteProp = serializedObject.FindProperty("data.icon");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            ItemDataHolder dataHolder = target as ItemDataHolder;
            
            GUILayout.Label("Item Name");
            dataHolder.data.name = EditorGUILayout.TextField(dataHolder.data.name);
            
            GUILayout.Label("Item Description");
            dataHolder.data.description = EditorGUILayout.TextField(dataHolder.data.description);
            
            GUILayout.Label("Max Stack Size");
            dataHolder.data.maxStack = EditorGUILayout.IntField(dataHolder.data.maxStack);
            
            GUILayout.Label("ID");
            dataHolder.data.id = EditorGUILayout.IntField(dataHolder.data.id);
            
            
            dataHolder.data.icon = (Sprite) EditorGUILayout.ObjectField("Item Icon", dataHolder.data.icon, typeof(Sprite));


            //dataCreator.data.icon = selectedSprite;

        }
    }
}