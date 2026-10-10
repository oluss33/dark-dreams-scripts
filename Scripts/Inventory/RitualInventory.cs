using UnityEngine;

namespace Scripts.Inventory
{
    public class RitualInventory : EntityInventory
    {
    
        public int maxSlots;

        private ItemData[] dataOnStart;
        private int[] amountsOnStart;

        public ItemData[] expectedItemData;
        
        public InventoryEvents.OnInventoryUpdate onRitualUpdate;
        

        private void Start()
        {
            inventory.maxSlots = maxSlots;


            /* 
             for (int i = 0; i < itemsToPutOnStart.Length; i++)
             {
                 inventory.AddItem(itemsToPutOnStart[i].data, itemsToPutOnStart[i].amount);
                 onUpdate?.Invoke(inventory, itemsToPutOnStart[i], InventoryEvents.InventoryEventType.Add);
             }
             */
        }

        public void SetupRitual(ItemData[] dataOnStart, int[] amountsOnStart, ItemData[] expectedItemData)
        {
            this.expectedItemData = dataOnStart;
            this.dataOnStart = dataOnStart;
            this.amountsOnStart = amountsOnStart;
        }

        public void Initialize()
        {
            for (int i = 0; i < dataOnStart.Length; i++)
            {
                Item item = new Item(dataOnStart[i], amountsOnStart[i]);
                inventory.AddItem(item.data, item.amount);
                onRitualUpdate?.Invoke(inventory, item, InventoryEvents.InventoryEventType.Add);
                Debug.Log("Dodano item " + item.data.name);
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