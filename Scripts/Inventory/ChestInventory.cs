using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Inventory
{
    public class ChestInventory : EntityInventory
    {
        // TODO: another script to manage
        public int maxSlots;
        public Item[] itemsToPutOnStart;

        public InventoryEvents.OnChestUpdate onChestUpdate;


        private void Start()
        {
            
            for (int i = 0; i < itemsToPutOnStart.Length; i++)
            {
                inventory.AddItem(itemsToPutOnStart[i].data, itemsToPutOnStart[i].amount);
                onChestUpdate?.Invoke(inventory, itemsToPutOnStart[i], InventoryEvents.InventoryEventType.Add);
            }
        }

        public override int AddItem(ItemData data, int amount)
        {
            int addedAmount = inventory.AddItem(data, amount);
            Debug.Log(addedAmount);
            /*if (addedAmount > 0)
            {
                onChestUpdate?.Invoke(inventory, new Item(data, amount), InventoryEvents.InventoryEventType.Add);
            }*/

            return addedAmount;
        }

        public override int RemoveItem(ItemData data, int amount)
        {
            int removedAmount = inventory.RemoveItem(data, amount);
            Debug.Log(removedAmount);
            if (removedAmount > 0)
            {
                Debug.Log("Usunieto prawidlowo");
            }

            return removedAmount;
        }
        

        public void PutItemFromOtherInventory(Inventory otherInventory, Item addedItem)
        {
            AddItem(addedItem.data, addedItem.amount);
            Debug.Log(otherInventory.RemoveItem(addedItem.data, addedItem.amount));
            otherInventory.RemoveItem(addedItem.data, addedItem.amount);
        }

        public void TakeItemToOtherInventory(Inventory otherInventory, Item addedItem)
        {
            RemoveItem(addedItem.data, addedItem.amount);
            otherInventory.AddItem(addedItem.data, addedItem.amount);
        }
    }
}