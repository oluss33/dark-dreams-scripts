using System;
using UnityEngine;

namespace Scripts.Inventory
{
    public class RitualInventoryUI : InventoryUI
    {
        [SerializeField] private Slot[] slots;
        [SerializeField] private RitualInventory inv;

        [SerializeField] public Slot[] ritualSlots;

        private bool ritualCompleted = false;

        public delegate void OnRitualCompleted();

        public OnRitualCompleted onRitualCompleted;

        void Awake()
        {
            inv.onRitualUpdate += OnInventoryChange;
        }

        private void Start()
        {
            InitializeUI();
            for (int i = 0; i < ritualSlots.Length; i++)
            {
                ritualSlots[i].SetEmptyItem();
            }
            gameObject.SetActive(false);
        }

        void OnInventoryChange(Inventory source, Item item, InventoryEvents.InventoryEventType type)
        {
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

        bool Check()
        {
            for (int i = 0; i < ritualSlots.Length; i++)
            {
                Slot slot = ritualSlots[i];
                if (slot.currentItem.data.id != inv.expectedItemData[i].id)
                {
                    Debug.Log("Złe id w slocie " + slot.gameObject.name + ": " + slot.currentItem.data.id +
                              "\nOczekiwane id: " + inv.expectedItemData[i].id);
                    return false;
                }
            }
            return true;
        }


        public void CheckRitual()
        {
            Debug.Log(Check());
            if (Check())
            {
                Debug.Log("Ritual: Completed");
                onRitualCompleted?.Invoke();
            }
        }
        
        public override void InitializeUI()
        {
            /*for (int i = 0; i < slots.Length; i++)
            {
                Slot slotComponent = slots[i].GetComponent<Slot>();
                slotComponent.SetEmptyItem();
            }*/
            //inv.InitializeTest();
        }

        public override void Show()
        {
            throw new System.NotImplementedException();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override bool isShown()
        {
            throw new System.NotImplementedException();
        }

        public override void OnItemAdded(Item item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].isEmpty)
                {
                    Debug.Log(item.data.name + slots[i].gameObject.name);
                    slots[i].SetItem(item.data, item.amount, item.data.icon, item, false);
                    return;
                }
            }
        }

        public override void OnItemRemoved(Item item)
        {
            Debug.Log("nara");
        }
    }
}