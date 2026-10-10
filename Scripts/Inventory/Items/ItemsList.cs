 using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    /// <summary>
    /// List of items in the game
    /// </summary>
    public class ItemsList : MonoBehaviour
    {
        [HideInInspector] public ItemData stick;
        [HideInInspector] public ItemData sword;
        [HideInInspector] public ItemData key;
        [HideInInspector] public ItemData sweet;
        [HideInInspector] public ItemData spiderHeart;
        [HideInInspector] public ItemData candle;
        [HideInInspector] public ItemData torch;
        [HideInInspector] public ItemData bow;
        
        public List<ItemData> datas = new List<ItemData>();

        [SerializeField] private ItemDataHolder[] datasObject;

        public static ItemsList instance { get; private set; }

        private void Awake()
        {
            if (instance != null) Debug.LogError("More than 1 ItemsList instance", this);
            instance = this;
            Init();
        }
        public void Init()
        {

            // Set scriptableobject variables to items

            for (int i = 0; i < datasObject.Length; i++)
            {
                datas.Add(datasObject[i].data);
            }
            
            // Initialize items
            InitializeItems();
        }

        /// <summary>
        /// This function sets items and OnUse Actions
        /// </summary>
        void InitializeItems()
        {
            // Stick
            stick = datas.Find(data => data.name == "Stick");
            
            // Sword
            sword = datas.Find(data => data.name == "Sword");

            key = datas.Find(data => data.name == "Key");

            sweet = datas.Find(data => data.name == "Sweet");

            spiderHeart = datas.Find(data => data.name == "Spider Heart");
            
            candle = datas.Find(data => data.name == "Candle");

            torch = datas.Find(data => data.name == "Torch");

            bow = datas.Find(data => data.name == "Bow");
        }
    }
}