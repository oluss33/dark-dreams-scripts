using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public Inventory inventory;

        public InventoryEvents.OnInventoryUpdate onUpdate;

        public InventoryManager(Inventory inventory)
        {
            this.inventory = inventory;
        }
        
        public int AddItem(ItemData data, int amount)
        {
            int addedAmount = inventory.AddItem(data, amount);
            if (addedAmount > 0)
            {
                onUpdate?.Invoke(inventory, new Item(data, amount), InventoryEvents.InventoryEventType.Add);
            }

            return addedAmount;
        }

        public int RemoveItem(ItemData data, int amount)
        {
            int removedAmount = inventory.RemoveItem(data, amount);
            if (removedAmount > 0)
            {
                onUpdate?.Invoke(inventory, new Item(data, amount), InventoryEvents.InventoryEventType.Remove);
            }

            return removedAmount;
        }
    }
}
