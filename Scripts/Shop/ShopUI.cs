using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Inventory;
using Scripts.Shop;
using TMPro;

namespace Scripts.Shop
{
    public class ShopUI : MonoBehaviour
    {
        public ShopSlotButton[] shopSlots;
        public Transform shopSlotContainer;
        public GameObject shopSlotPrefab;

        public Text shopNameUI;
        public Text coinCountUI;

        public Shop currentShop;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ResetContent()
        {
            if(shopSlots.Length == 0) return;
            else
            {
                foreach(ShopSlotButton _shopSlot in shopSlots)
                {
                    Destroy(_shopSlot.gameObject);
                }
                shopSlots = new ShopSlotButton[0];
            }
        } 

        public void GenerateContent(List<Item> itemsList)
        {
            shopNameUI.text = currentShop.name;
            ResetContent();
            int index = 0;
            shopSlots = new ShopSlotButton[itemsList.Count];
            foreach(Item _item in itemsList)
            {
                GameObject newShopSlotObj = Instantiate(shopSlotPrefab, shopSlotContainer);
                ShopSlotButton shopSlotButton = newShopSlotObj.GetComponent<ShopSlotButton>();

                shopSlotButton.UpdateItem(_item);
                shopSlotButton.shopUI = this;
                shopSlotButton.itemIndex = index;


                shopSlots[index] = shopSlotButton;

                index++;
            }
        }

        public void UpdateContent(List<Item> itemsList)
        {
            ResetContent();
            GenerateContent(itemsList);
        }

        public void OpenShop()
        {
            this.gameObject.SetActive(true);
            coinCountUI.text = "x" + currentShop.GetMoneyCount().ToString(); 
        }
        public void CloseShop()
        {
            this.gameObject.SetActive(false);
        }

    }
}