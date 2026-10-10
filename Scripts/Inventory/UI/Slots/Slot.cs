using System;
using Scripts.Inventory.ItemFunctions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Scripts.Inventory
{
    public class Slot : MonoBehaviour
    {
        public Transform itemParent;
        public Text uiName;
        public Text uiTextCount;
        public Image uiImage;
        public Item currentItem;
        public bool isEmpty;
        public int uiCount;
        public Vector2 startPos;
        public EntityInventory entityInv;
        public Button equipBTN;

        public enum SlotType
        {
            Inventory,
            Equipment,
            Chest,
            Ritual
        }

        public SlotType type;
        
        [SerializeField] public PlayerInventoryUI ui;
        [SerializeField] private Transform container;

        public void SetItem(ItemData data, int uiCount, Sprite uiImage, Item currentItem, bool isEmpty)
        {
            
            this.uiName.text = data.name;
            uiTextCount.text = uiCount.ToString();
            this.uiImage.sprite = uiImage;
            this.uiImage.preserveAspect = true;

            
            this.currentItem = currentItem;
            this.currentItem.amount = uiCount;
            this.currentItem.data = data;
            this.currentItem.data.name = data.name;
            this.currentItem.data.id = data.id;
            
//            Debug.Log(data.id);
            
            this.isEmpty = isEmpty;
            this.uiCount = uiCount;
            
            
            itemParent.name = currentItem.data.name;

           // if (!this.isEmpty) this.uiImage.raycastTarget = true;
             this.uiImage.raycastTarget = false;

             if (uiImage == null)
                 this.uiImage.color = new Color(255, 255, 255, 0);
             else
                 this.uiImage.color = new Color(255, 255, 255, 255);

            if (uiCount == 0) this.uiTextCount.text = String.Empty;

            if (type == SlotType.Inventory)
            {
                equipBTN.gameObject.SetActive(!isEmpty && currentItem.data.equipable);
            }
            else
            {
                equipBTN.gameObject.SetActive(false);
            }
            
        }

        public void OnClick()
        {
            // UseItem Use Using Items    
            if (!currentItem.data.isUsable) return;
            ui.currentSlot = this;
            ItemFunctionality.GetItemFunction(currentItem.data).Use();
            ui.entityInventory.RemoveItem(currentItem.data, 1);
        }
        

        public void InitPosition()
        {
            startPos = itemParent.transform.position;
        }
        
        public string GetItemName()
        {
            return currentItem.data.name;
        }
        
        public void SetEmptyItem()
        {
            SetItem(new ItemData(), 0, null, new Item(), true);
        }

        /*public void OnBeginDrag(PointerEventData eventData)
        {
            if (isEmpty) return;
            startPos = itemParent.transform.position;
            itemParent.SetParent(container.parent);
            itemParent.SetAsLastSibling();
        }*/

        bool CheckNewInventorySlot(Slot newSlot)
        {
            //Debug.Log("Znalazlem nowy InventorySlot!");
            if (newSlot.isEmpty && newSlot != this)
            {
                // Debug.Log("Nowy slot jest pusty i nie jest takis sam!");
                newSlot.SetItem(this.currentItem.data, this.uiCount, this.currentItem.data.icon,
                    this.currentItem, false);
                SetEmptyItem();
                itemParent.transform.position = startPos;
                return true;
            }
            else if (!newSlot.isEmpty && newSlot.currentItem.data.name != currentItem.data.name || newSlot.uiCount == currentItem.data.maxStack)
            {
                // Debug.Log("Nowy slot nie jest pusty i ma inny item");

                itemParent.transform.position = startPos;

                Item tempItem = currentItem;
                Item newSlotItem = newSlot.currentItem;
                int tempUiCount = uiCount;

                SetItem(newSlotItem.data, newSlot.uiCount, newSlotItem.data.icon, newSlotItem, false);
                newSlot.SetItem(tempItem.data, tempUiCount, tempItem.data.icon, tempItem, false);
                return true;
            }
            else if (!newSlot.isEmpty && newSlot.currentItem.data.name == currentItem.data.name && newSlot != this)
            {
                // Debug.Log("Nowy slot nie jest pusty i taki sam item");

                itemParent.transform.position = startPos;

                if(uiCount + newSlot.uiCount <= currentItem.data.maxStack)
                {
                    newSlot.SetItem(currentItem.data, uiCount + newSlot.uiCount, currentItem.data.icon, currentItem, false);
                    SetEmptyItem();
                }
                else
                {
                    int residue = uiCount + newSlot.uiCount - currentItem.data.maxStack;
                    newSlot.SetItem(currentItem.data, currentItem.data.maxStack, currentItem.data.icon, currentItem, false);
                    SetItem(currentItem.data, residue, currentItem.data.icon, currentItem, false);
                }

                return true;
            }
            else if (newSlot == this)
            {
                itemParent.transform.position = startPos;
                return true;
            }

            return false;
        }

        bool CheckNewChestSlot(Slot newSlot)
        {
            if (newSlot == this) return false;
            newSlot.SetItem(currentItem.data, currentItem.amount, currentItem.data.icon, currentItem, false);
            //if(newSlot != this)
                SetEmptyItem();
            itemParent.transform.position = startPos;
            return true;
        }
        
        /*void CheckNewEquipmentSlot(Slot newSlot)
        {
            if (newSlot == this)
            {
                itemParent.transform.position = startPos;
                return;
            }
//            Debug.Log("Znalazlem nowy EquipmentSlot!");
            EquipmentSlot equipmentSlot = newSlot.GetComponent<EquipmentSlot>();

            if (equipmentSlot.segmentType == EquipmentSlot.SegmentType.Body && currentItem.data.type == ItemType.Body ||
                equipmentSlot.segmentType == EquipmentSlot.SegmentType.Head && currentItem.data.type == ItemType.Head || 
                equipmentSlot.segmentType == EquipmentSlot.SegmentType.Legs && currentItem.data.type == ItemType.Legs ||
                equipmentSlot.segmentType == EquipmentSlot.SegmentType.Neck && currentItem.data.type == ItemType.Neck || 
                equipmentSlot.segmentType == EquipmentSlot.SegmentType.Hand && currentItem.data.type == ItemType.Hand)
            {
                if (newSlot == this)
                {
                    itemParent.transform.position = startPos; return;}
                if (newSlot.container.parent.GetComponent<PlayerInventoryUI>().eqInventory.EquipItem(newSlot, currentItem.data))
                {
                    newSlot.SetItem(currentItem.data, 1, currentItem.data.icon, currentItem, false);
                    SetEmptyItem();
                    Debug.Log("dało radę");
                    itemParent.transform.position = startPos;
                }
            }
            else
            {
                Debug.Log("Nie dalo rady XD");
                itemParent.transform.position = startPos;
            }
        }
        */

        /*public void OnEndDrag(PointerEventData eventData)
        {
//            Debug.Log(isEmpty + " empty");
//            Debug.Log(eventData.pointerEnter + " hit");
            if (isEmpty) return;
            if (eventData.pointerEnter != null)
            {
                Slot newSlot = eventData.pointerEnter.GetComponent<Slot>();
                if (newSlot != null)
                {
                    //if (type == SlotType.Equipment && newSlot != this)
                     //   container.GetComponentInParent<PlayerInventoryUI>().eqInventory
                       //     .UnEquipItem(this, currentItem.data);
                    
                    if (newSlot.type == SlotType.Inventory)
                    {
                        if (CheckNewInventorySlot(newSlot))
                        {
                            if (type == SlotType.Chest)
                            {
                                ChestInventory c = container.GetComponentInParent<ChestInventory>();
                                Inventory slotInv = newSlot.GetComponent<InventorySlot>().ui.entityInventory.inventory;
                                
                                c.TakeItemToOtherInventory(slotInv, newSlot.currentItem);
                            }
                        }
                    }
                    else if (newSlot.type == SlotType.Equipment)
                    {
                        print("DEPRECATED FUNCTIONALITY!!!");
                        //CheckNewEquipmentSlot(newSlot);
                    }
                    else if (newSlot.type == SlotType.Chest)
                    {
                        if (CheckNewChestSlot(newSlot))
                        {
                            Debug.Log(newSlot.currentItem.data.name);
                            Debug.Log(newSlot.currentItem.amount);
                            newSlot.container.GetComponentInParent<ChestInventory>().PutItemFromOtherInventory(newSlot.entityInv.inventory, newSlot.currentItem);
                        }
                    }
                    else if (newSlot.type == SlotType.Ritual)
                    {
                        Debug.Log("New slot type is ritual");
                        
                        itemParent.transform.position = startPos;
                        
                        newSlot.SetItem(currentItem.data, currentItem.amount, currentItem.data.icon, currentItem, true);
                        SetEmptyItem();
                        newSlot.container.GetComponentInParent<RitualInventoryUI>().CheckRitual();
                    }
                }

                else
                {
                    itemParent.transform.position = startPos;
                }
            }
            else
            {
                itemParent.transform.position = startPos;
            }
            uiImage.raycastTarget = false;
            itemParent.SetParent(transform);
        }
*/
        /*public void OnDrag(PointerEventData eventData)
        {
            if (isEmpty) return;
            itemParent.position = Input.mousePosition;
        }*/
    }
}