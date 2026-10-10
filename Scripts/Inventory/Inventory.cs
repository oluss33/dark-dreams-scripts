using System.Collections.Generic;
using Scripts.Inventory.ItemFunctions;
using UnityEngine;

namespace Scripts.Inventory
{
    public class Inventory
    {
        public List<Item> itemsList = new List<Item>();
        public List<Item> equippedItemsList = new List<Item>();

        public int maxSlots;
        public int equipmentSlots;

        public bool isFull;

        public bool EquipItem(Item item)
        {
            if (item.isEquipped) return false;
            if (item.data.equipable)
            {
                equippedItemsList.Add(item);
                item.isEquipped = true;


                item.data.onEquip?.Invoke();
                ItemFunctionality.GetItemFunction(item.data).Equip();

                if (item.data.type == ItemType.Hand)
                {
                    if (!GameManager.Instance.equippedWeapon) {
                        GameManager.Instance.equippedWeapon = true;
                        Hints.instance.ShowHint(Player.Player.main.gameObject, "Weapon", "Press Z to draw your weapon", 2); 
                    }
                }

                return true;
            }

            return false;
        }

        public bool IsEquipped(ItemData data)
        {
            return EquipmentIndex(data) >= 0;
        }

        public void UnEquipItem(Item item)
        {
            int index = EquipmentIndex(item.data);
            equippedItemsList.Remove(equippedItemsList[index]);
            item.isEquipped = false;
            ItemFunctionality.GetItemFunction(item.data).UnEquip();
        }
        
        public int AddItem(ItemData data, int amount)
        {
            if (isFull)
            {
                Debug.Log("Ekwipunek pełny");
                return 0;
            } 
            if (!HasItem(data))
            {
                if (HasFreeSlots())
                {
                    itemsList.Add(new Item(data, amount));
                    return amount;
                }
            }
            else
            {
                int index = ItemIndex(data);
                Item foundItem = itemsList[index];
                foundItem.amount += amount;
                itemsList[index] = foundItem;
                return amount;
                    
            }
            return 0;
        }

        public int RemoveItem(ItemData data, int amount)
        {
            if (HasItem(data))
            {
                int index = ItemIndex(data);
                Item foundItem = itemsList[index];
                if (foundItem.amount >= amount)
                {
                    foundItem.amount -= amount;
                    //foundItem.RemoveAmount(amount);
                    itemsList[index] = foundItem;

                    if (itemsList[index].amount <= 0)
                    {
                        ItemData data1 = itemsList[index].data;
                        Debug.Log("I'm removing item from the ItemsList");
                        itemsList.Remove(itemsList[index]);
                    }
                    return amount;
                }
            }
            return 0;

        }

        public bool HasItem(ItemData data)
        {
            return ItemIndex(data) >= 0;
        }
        

        bool HasFreeSlots()
        {
            if (itemsList.Count >= maxSlots)
            {
                Debug.Log("You do not have free space in Inventory!");
                return false;
            }
    
            return true;
        }

        public int ItemIndex(ItemData data)
        {
            return itemsList.FindIndex(i => i.data.name == data.name);
        }

        int EquipmentIndex(ItemData data)
        {
            return equippedItemsList.FindIndex(i => i.data.name == data.name);
        }

        public void ShowContent()
        {
            Debug.Log("Pokazuję spis itemów w ekwipunku");
            foreach (Item item in itemsList)
            {
                Debug.Log(item.data.name + " " + item.amount);
            }
        }
        
        public int GetIndexById(int id)
        {
            for(int i = 0; i < itemsList.Count; i++)
            {
                if(itemsList[i].data.id == id)
                {
                    return i;
                }
            }
            Debug.Log($"No such item with ID: '{id}' found.");
            return -1;
        }

    }
}