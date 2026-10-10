using System;
using UnityEngine;

namespace Scripts.Inventory
{
    public class ChestInventoryUI : InventoryUI
    {
        [SerializeField] private Slot[] slots;
        [SerializeField] private ChestInventory inv;

        void Awake()
        {
            inv.onChestUpdate += OnInventoryChange;
        }


        void OnInventoryChange(Inventory source, Item item, InventoryEvents.InventoryEventType type)
        {
            Debug.Log("wywoluje cokolwiek");
            switch (type)
            {
                case InventoryEvents.InventoryEventType.Add:
                    OnItemAdded(item);
                    break;
                case InventoryEvents.InventoryEventType.Remove:
                    OnItemRemoved(item);
                    break;
            }
        }
        
        public override void InitializeUI()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                Slot slotComponent = slots[i].GetComponent<Slot>();
                slotComponent.SetEmptyItem();
            }
        }

        public override void Show()
        {
            throw new System.NotImplementedException();
        }

        public override void Hide()
        {
            throw new System.NotImplementedException();
        }

        public override bool isShown()
        {
            throw new System.NotImplementedException();
        }

        public override void OnItemAdded(Item item)
        {
            Debug.Log("dodaje item");
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].isEmpty)
                {
                    Debug.Log("slot jest pusty");
                    slots[i].SetItem(item.data, item.amount, item.data.icon, item, false);
                }
            }
        }

        public override void OnItemRemoved(Item item)
        {
            Debug.Log("nara");
        }
    }
}