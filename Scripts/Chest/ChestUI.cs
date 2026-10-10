using System;
using System.Collections.Generic;
using Scripts.Inventory;
using Scripts.Player;
using Scripts.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Chest
{
    public class ChestUI : MonoBehaviour, Openable
    {

        [SerializeField] private Transform itemsUI;
        
        public Chest currentChest;
        public GameObject itemPrefab;
        
        public List<Item> itemsList = new List<Item>();
        public List<GameObject> buttons = new List<GameObject>();

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void GenerateItem(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                GameObject itemGO = Instantiate(itemPrefab, itemsUI);
                ChestSlot slot = itemGO.GetComponent<ChestSlot>();
                slot.Setup(currentChest, item, i);
                slot.name = "Slot " + i;
                
                buttons.Add(itemGO);
                itemsList.Add(item);
            }
        }

        public void GetItem(int index)
        {
            
            // todo zabezpieczenie przed inventoryFull
            Item i = new Item(itemsList[index].data, itemsList[index].amount);
            Debug.Log("I'm trying to buy: " + i.data.name);
            if (Player.Player.main.inventory.entityInventory.AddItem(itemsList[index].data, itemsList[index].amount) > 0)
            {
                Debug.Log("I bought: " + itemsList[index].data.name);
                Destroy(buttons[index].gameObject);
                buttons.RemoveAt(index);
                itemsList.RemoveAt(index);
                currentChest.GetItem(index);
            }
            else
            {
                TooltipWarning.ShowTooltip_Static("You don't have enough space in inventory!");
                
            }
        }

        public void Back()
        {
            gameObject.SetActive(false);
        }

        public bool IsOpened()
        {
            return gameObject.activeSelf;
        }
    }
}