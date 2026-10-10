using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    [DefaultExecutionOrder(-900)]
    public class EntityInventory : MonoBehaviour
    {
        public Inventory inventory;

        public InventoryEvents.OnInventoryUpdate onUpdate;
        
        // TAGI: BUGFIX TODO JESLI COS SIE ZJEBALO Z INVENTORY I JEST NULLEM W JAKIMS KODZIE BARDZO MOZLIWE ZE PRZEZ TO ZE TA FUNKCJA JEST AWAKE. POPRZEDNO BYLA NA START,
        // JESLI COS NIE DZIALA;
        // : SPRAWDZ TO!
        // ^^poki co dzialaa
        private void Awake()
        {
            inventory = new Inventory();
        }

        public void SetupInventory(List<Item> items)
        {
            Debug.Log("I got new items from another scene!");
            for (int i = 0; i < items.Count; i++)
            {
                AddItem(items[i].data, items[i].amount);
            }
        }
        

        public virtual int AddItem(ItemData data, int amount)
        {
            Debug.Log(gameObject.name);
            Debug.Log(inventory);
            int addedAmount = inventory.AddItem(data, amount);
            if (addedAmount > 0)
            {
//                Debug.Log("Gówno\nData: " + data.name  + " \nID: " + data.id);
                onUpdate?.Invoke(inventory, new Item(data, amount), InventoryEvents.InventoryEventType.Add);
            }

            return addedAmount;
        }

        public virtual int RemoveItem(ItemData data, int amount)
        {
            int removedAmount = inventory.RemoveItem(data, amount);
            if (removedAmount > 0)
            {
                onUpdate?.Invoke(inventory, new Item(data, amount), InventoryEvents.InventoryEventType.Remove);
            }

            return removedAmount;
        }
        
        public virtual void ShowContent()
        {
            inventory.ShowContent();
        }

        public virtual int GetAmountById(int id)
        {
            if(inventory.GetIndexById(id) >= 0)
                return inventory.itemsList[inventory.GetIndexById(id)].amount;
            else 
                return 0;
        }

        public virtual int RemoveItemByID(int id, int amount)
        {
            int index = inventory.GetIndexById(id);
            if(index >= 0)
                return RemoveItem(inventory.itemsList[index].data, amount);
            else return -1;
        }
    }
}