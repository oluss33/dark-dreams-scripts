using UnityEngine.UI;
using UnityEngine;

namespace Scripts.Inventory
{
    public class InventorySlot : Slot
    {
        public Button removeBTN;
        public bool isFull;

        Sprite equipSprite;
        Sprite unEquipSprite;

        private void Start()
        {

            equipSprite = Resources.Load<Sprite>("Textures/Inventory Textures/tile_0107");
            unEquipSprite = Resources.Load<Sprite>("Textures/Inventory Textures/tile_0209");

            equipBTN.GetComponent<Image>().sprite = equipSprite;
        }        

        public void DropItem(EntityInventory entityInventory)
        {
            ui.currentSlot = this;
            Debug.Log("I am trying to drop " + currentItem.data.name);
            print(isEmpty);
            if(!isEmpty)
               // entityInventory.RemoveItem(currentItem.data, 1);
               ui.DropItem(currentItem.data, 1);
        }

        public void EquipItem()
        {
            print("lecymy");
            if (isEmpty) return;
            if (!currentItem.data.equipable) return;
            print(currentItem.data.name);
            if(currentItem.isEquipped)
            {
                ui.inventory.UnEquipItem(currentItem);
                equipBTN.GetComponent<Image>().sprite = equipSprite;
            }
            else
            {
                ui.inventory.EquipItem(currentItem);
                equipBTN.GetComponent<Image>().sprite = unEquipSprite;
            }
            /*if (!ui.eqInventory.inventory.IsEquipped(currentItem.data)) {
                print("warunek 1");
                ui.inventory.EquipItem(currentItem);
                equipBTN.GetComponent<Image>().sprite = unEquipSprite;
            }
            else
            {
                print("warunek 2");
                ui.eqInventory.UnEquipItem(this, currentItem);
                equipBTN.GetComponent<Image>().sprite = equipSprite;
            }
            */
        }

    }
}