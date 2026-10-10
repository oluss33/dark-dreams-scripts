using System;
using Scripts.Combat.Player;
using Scripts.Inventory.ItemFunctions;
using UnityEngine;

namespace Scripts.Inventory
{
    /*
    public class EquipmentInventory
    {
       public Inventory inventory;

       public EquipmentInventory(Inventory inventory)
       {
           this.inventory = inventory;
       }
       
        public InventoryEvents.OnEquipmentUpdate onUpdate;

        public bool EquipItem(Slot slot, Item item)
        {
            Debug.Log("EquipmentInventory: Equipping Item");
            if (inventory.EquipItem(item.data))
            { 
                item.isEquipped = true;
                //onUpdate?.Invoke(inventory, new Item(data, 1), slot, InventoryEvents.InventoryEventType.Add);
                item.data.onEquip?.Invoke();
//                Debug.Log("ID: " + data.id);
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

        public void UnEquipItem(Slot slot, Item item)
        {
            Debug.Log("EquipmentInventory: UnEquipping Item");
            inventory.UnEquipItem(item.data);
//            data.onUnEquip?.Invoke();
            ItemFunctionality.GetItemFunction(item.data).UnEquip();
            Debug.Log("UnEquip: Done");
            item.isEquipped = false;
            //onUpdate?.Invoke(inventory, new Item(data, 1),slot, InventoryEvents.InventoryEventType.Remove);
        }
    }*/
}