using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Inventory;
using Scripts.Tooltip;

namespace Scripts.Shop
{
    public class Shop : MonoBehaviour
    {
        public string shopName;

        //Inspector Items on start
      //  public ItemData[] itemDatas;
        public ItemDataHolder[] holders;
        //public int[] amounts;

        public Dictionary<ItemDataHolder, int> a = new Dictionary<ItemDataHolder, int>();

        //Items
        public List<Item> itemsList = new List<Item>();

        //UI
        public ShopUI shopUI;

        void Start()
        {
            foreach (ItemDataHolder holder in holders)
            {
                itemsList.Add(new Item(holder.data, 1));
            }
        }

        void OpenShop()
        {
            if(shopUI.currentShop != this)
            {
                shopUI.currentShop = this;
                shopUI.GenerateContent(itemsList);
            }
            else
            {
                shopUI.UpdateContent(itemsList);
            }

            shopUI.OpenShop();
        }
        public void BuyItem(int itemIndex)
        {
            Item item = itemsList[itemIndex];

            EntityInventory entityInventory = Player.Player.main.inventory.entityInventory;
            int coins = GetMoneyCount(); 


            if(coins >= item.data.cost)
            {
                if(itemsList[itemIndex].amount > 1)
                {
                    itemsList[itemIndex] = new Item(item.data, item.amount - 1);
                }
                else itemsList.RemoveAt(itemIndex);

                entityInventory.RemoveItemByID(4, item.data.cost); // Coin's id = 4

                entityInventory.AddItem(item.data, 1);

                shopUI.UpdateContent(itemsList);
            }
            else
            {
                Tooltip.TooltipWarning.ShowTooltip_Static("You don't have enough money");
            }
        }

        public int GetMoneyCount()
        {
            EntityInventory entityInventory = Player.Player.main.inventory.entityInventory;
            int coins = entityInventory.GetAmountById(4); // Coin's id = 4
            return coins;
        }

        public void ChangeStartItems()
        {
            /*
            itemDatas = new ItemData[itemsList.Count];
            amounts = new int[itemsList.Count];

            for(int i = 0; i < itemsList.Count; i++)
            {
                itemDatas[i] = itemsList[i].data;
                amounts[i] = itemsList[i].amount;
            }
            */
        }

        private void OnMouseDown() {
            OpenShop();
        }
    }
}


