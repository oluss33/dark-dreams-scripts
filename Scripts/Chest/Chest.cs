using System;
using System.Collections.Generic;
using Scripts.Inventory;
using UnityEngine;

namespace Scripts.Chest
{
    public class Chest : MonoBehaviour
    {

        public ChestUI ui;
        
        public ItemData[] itemDatas;
        public int[] amounts;
        public List<Item> itemsList = new List<Item>();

        private bool itemsCreated = false;

        [SerializeField] string password;

        private void Start()
        {
            foreach (ItemData data in itemDatas)
            {
                foreach (int amount in amounts)
                {
                    itemsList.Add(new Item(data, amount));
                }
            }
        }

        private void OnMouseDown()
        {
            if (password != "") { ChestPassword.instance.SetPassword(password, this); ChestPassword.instance.gameObject.SetActive(true);}
            else
            {
                OpenChest();
            }

        }

        public void GetItem(int index)
        {
            itemsList.RemoveAt(index);
        }

        public void OpenChest()
        {
            ui.currentChest = this;
            ui.gameObject.SetActive(true);
            if (!itemsCreated)
            {
                ui.GenerateItem(itemsList);
            }
            itemsCreated = true;
        }
    }
}