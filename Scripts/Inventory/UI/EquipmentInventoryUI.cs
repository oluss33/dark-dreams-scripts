using System;
using UnityEngine.UI;
using UnityEngine;

namespace Scripts.Inventory
{
    public class EquipmentInventoryUI : InventoryUI
    {

        //public EquipmentInventory equipmentInventory;
        
        [SerializeField] private Transform equipmentContainer;
        
        [SerializeField] private GameObject[] slots;

        private void Start()
        {
            InitializeUI();
           // equipmentInventory.onUpdate += OnInventoryChange;
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
            throw new System.NotImplementedException();
        }

        public override void OnItemRemoved(Item item)
        {
            throw new System.NotImplementedException();
        }
    }
}