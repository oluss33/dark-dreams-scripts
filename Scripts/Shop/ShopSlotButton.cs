using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Inventory;
using Scripts.Shop;
using UnityEngine.UI;
using TMPro;

namespace Scripts.Shop
{
    public class ShopSlotButton : MonoBehaviour
    {
        public Item currentItem;
        public ShopUI shopUI;
        public int itemIndex;

        public TextMeshProUGUI nameView;
        public TextMeshProUGUI costView;
        public TextMeshProUGUI countView;

        public Image icon;
        public void UpdateItem(Item item)
        {
            currentItem = item;
            nameView.text = item.data.name;
            countView.text = "x" + item.amount.ToString();
            costView.text = item.data.cost.ToString() + "$";
            icon.sprite = item.data.icon;
            icon.preserveAspect = true;

            Button button = GetComponentInChildren<Button>();
            button.onClick .AddListener(delegate { BuyItem(); });
        }

        public void BuyItem()
        {
            shopUI.currentShop.BuyItem(itemIndex);
            shopUI.coinCountUI.text = "x" + shopUI.currentShop.GetMoneyCount().ToString(); 

        }


    }
}
