using System;
using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Inventory
{
    public class ChestInventoryManager : MonoBehaviour
    {
        public ChestInventory chestInv;
        public int[] amounts;
        public ItemData[] datas;

        private void Awake()
        {
            chestInv.itemsToPutOnStart = new Item[chestInv.maxSlots];
            chestInv.inventory.maxSlots = chestInv.maxSlots;
            for (int i = 0; i < datas.Length; i++)
            {
                for (int j = 0; j < amounts.Length; j++)
                {
                    chestInv.itemsToPutOnStart[i] = new Item(datas[i], amounts[j]);
                    Debug.Log(chestInv.itemsToPutOnStart[i].data.name + ": " + chestInv.itemsToPutOnStart[i].amount);
                }
            }
        }
    }
}