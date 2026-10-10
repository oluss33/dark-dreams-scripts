using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Scripts.Inventory
{
    [DefaultExecutionOrder(-600)]
    public class PlayerInventoryUI : InventoryUI
    {
        //  public Inventory inventory;
        public EntityInventory entityInventory;
        public Inventory inventory;
        //public EquipmentInventory eqInventory;
        public int maxSlots;

        private ItemsList list;

        [SerializeField] private Transform inventoryContainer;

        [SerializeField] private GameObject[] slotsGO;
        [SerializeField] public List<Slot> slots = new List<Slot>();

        [SerializeField] private GameObject[] equipmentSlotsGO;
        [SerializeField] public List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
        [SerializeField] private TextMeshProUGUI actionText;
        
        public Slot currentSlot;

        private void Start()
        {

            actionText.gameObject.SetActive(false);

            inventory = entityInventory.inventory;
            actionText.text = "";
            
            list = ItemsList.instance;
            //list.Init();
            inventory.maxSlots = maxSlots;
            // inventory.equipmentSlots = 3;
            entityInventory.onUpdate += OnInventoryChange;

            
            InitializeUI();
            
        }

        void RemoveItem(InventorySlot slot)
        {
            ItemWorldSpawner.instance.SpawnItem(slot.currentItem, transform.position);
            slot.DropItem(entityInventory);
        }

        public void Test(InventorySlot slotComponent)
        {
            slotComponent.removeBTN.onClick.AddListener(delegate { RemoveItem(slotComponent); });
        }

        public override void InitializeUI()
        {
            for (int i = 0; i < slotsGO.Length; i++)
            {
                slotsGO[i].name = $"Slot ({i})";
                InventorySlot slotComponent = slotsGO[i].GetComponent<InventorySlot>();
                slots.Add(slotComponent);
                slotComponent.removeBTN.onClick.AddListener(delegate { RemoveItem(slotComponent); });
                slotComponent.SetEmptyItem();
                slotsGO[i].GetComponent<Button>().onClick.AddListener(slotComponent.OnClick);
            }

            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].InitPosition();
                slots[i].itemParent.transform.position = slots[i].startPos;
            }

            for (int i = 0; i < equipmentSlotsGO.Length; i++)
            {
                Slot slotComponent = equipmentSlotsGO[i].GetComponent<Slot>();
                slotComponent.SetEmptyItem();
                equipmentSlotsGO[i].name = $"EQ Slot ({i})";
            }

            //entityInventory.AddItem(list.stick, 5);
            // TODO: o chuj tu chodzi? | 01.06.2026
            //inventory.EquipItem(list.sword);
        }

        bool IsInventoryFull()
        {
            foreach (Slot slot in slots)
            {
                if (!slot.GetComponent<InventorySlot>().isFull) return false; 
            }
            return true;
        }

        void CheckSlot(Slot slot, Item item)
        {
            if (slot.currentItem.amount == item.data.maxStack)
            {
                slot.GetComponent<InventorySlot>().isFull = true;
                if (IsInventoryFull()) inventory.isFull = true;
//                Debug.Log(IsInventoryFull());
            }
            else
            {
                slot.GetComponent<InventorySlot>().isFull = false;
            }
        }

        public override void Show()
        {
            inventoryContainer.gameObject.SetActive(true);
        }

        public override void Hide()
        {
            inventoryContainer.gameObject.SetActive(false);
        }

        public override bool isShown()
        {
            return inventoryContainer.gameObject.activeSelf;
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
                case InventoryEvents.InventoryEventType.Equip:
                    OnItemEquipped(item);
                    break;
            }
        }


        private void ShowActionText(string text)
        {
            actionText.text = text;
            actionText.gameObject.SetActive(true);
            Invoke("HideActionText", 2f);
        }

        private void HideActionText()
        {
            actionText.gameObject.SetActive(false);
        }

        public override void OnItemAdded(Item item)
        {
            ShowActionText($"+{item.amount} {item.data.name}");
            Debug.Log("Probuje dodac item");
            ItemData data = item.data;
            Debug.Log("Data id: " + data.id);
            for (int i = 0; i < slots.Count; i++)
            {
                if (!slots[i].isEmpty)
                {
                    if (slots[i].currentItem.data.name == data.name && slots[i].uiCount != item.data.maxStack)
                    {
                        if (slots[i].uiCount + item.amount < item.data.maxStack)
                        {
                            slots[i].SetItem(data, slots[i].uiCount + item.amount, data.icon, item, false);
                            CheckSlot(slots[i], item);
                            inventory.isFull = IsInventoryFull();
                            return;
                        }
                        else if (slots[i].uiCount + item.amount == item.data.maxStack)
                        {
                            slots[i].SetItem(data, slots[i].uiCount + item.amount, data.icon, item, false);
                            CheckSlot(slots[i], item);
                            inventory.isFull = IsInventoryFull();
                            return;
                        }
                        else
                        {
                            int itemResidue = (slots[i].uiCount + item.amount - item.data.maxStack);
                            slots[i].SetItem(data, item.data.maxStack, data.icon, item, false);
                            CheckSlot(slots[i], item);
                            inventory.isFull = IsInventoryFull();
                            Debug.Log("Trzeba dodać resztę: " + itemResidue);
                            OnItemAdded(new Item(data, itemResidue));
                            return;
                        }
                    }
                    else continue;
                }
                else
                {
                    if (item.amount <= item.data.maxStack)
                    {
                        slots[i].SetItem(data, item.amount, data.icon, item, false);
                        CheckSlot(slots[i], item);
                        inventory.isFull = IsInventoryFull();
//                        Debug.Log(transform.parent.gameObject.name +" + " + gameObject.name + " + " + slots[i].gameObject.name);
                        return;
                    }
                    else
                    {
                        slots[i].SetItem(data, item.data.maxStack, data.icon, item, false);
                        OnItemAdded(new Item(data, item.amount - item.data.maxStack));
                        CheckSlot(slots[i], item);
                        inventory.isFull = IsInventoryFull();
                        return;
                    }
                }
            }

            // if there's anything left, drop it
            Debug.Log("Dropped: " + item.amount);
            DropItem(item.data, item.amount);
            inventory.isFull = true;
            return;
        }

        public void DropItem(ItemData data, int amount)
        {
            entityInventory.RemoveItem(data, amount); 
            ItemWorldSpawner.instance.SpawnItem(new Item(data, amount), /* TODO: Random value, propably check walls? | 01.06.2026 */ Player.Player.main.transform.position);
        }

        public void OnItemEquipped(Item item)
        {
            actionText.text = $"Equipped {item.data.name}";
            Debug.Log("Equipping item");
        }

        public override void OnItemRemoved(Item item)
        {
            //  bool remove_actionDone = false;
            //if (remove_actionDone) return;
            if(currentSlot == null) 
                currentSlot = GetFirstSlotWithItem(item);
                
            actionText.text = $"-{item.amount} {item.data.name}";
            Debug.Log("I'm removing item");
            int temp_amount = currentSlot.currentItem.amount - item.amount;
            if (temp_amount > 0)
            {
                currentSlot.SetItem(item.data, temp_amount, item.data.icon, item, false);
            }
            else
            {
                currentSlot.SetEmptyItem();
            }

            currentSlot.GetComponent<InventorySlot>().isFull = false;
            inventory.isFull = false;
        }
        Slot GetFirstSlotWithItem(Item item)
        {
            foreach (Slot slot in slots)
            {
                if(slot.currentItem.data.id == item.data.id)
                {
                    return slot;
                }
            }
            return null;
        }
    }
}