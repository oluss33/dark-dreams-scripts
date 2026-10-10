using System.Collections;
using UnityEngine;

namespace Scripts.Inventory
{
    public struct Item
    {
        public ItemData data;
        public int amount;
        public bool isEquipped;

        public Item(ItemData data, int amount)
        {
            this.data = data;
            this.amount = amount;
            this.isEquipped = false;
        }

        /*public void AddAmount(int amount)
        {
            this.amount += amount;
        }

        public void RemoveAmount(int amount)
        {
            this.amount -= amount;
        }*/
    }
}