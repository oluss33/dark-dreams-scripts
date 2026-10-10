using Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Chest
{
    public class ChestSlot : MonoBehaviour
    {
        public Chest currentChest;
        public Item currentItem;
        public int index;

        public Text nameText;
        public Text countText;
        public Image itemImage;

        public void Setup(Chest currentChest, Item currentItem, int index)
        {
            this.currentChest = currentChest;
            this.currentItem = currentItem;
            this.index = index;
            
            UpdateSlot();
        }

        public void UpdateSlot()
        {
            Debug.Log(currentItem.data.name);
            nameText.text = currentItem.data.name;
            countText.text = currentItem.amount.ToString();
            itemImage.sprite = currentItem.data.icon;

            itemImage.preserveAspect = true;

        }

        public void GetItem()
        {
            currentChest.ui.GetItem(index);
        }

    }
}