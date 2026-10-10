using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Scripts.Enemy;
using Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Mobs
{
    public class Looting : MonoBehaviour
    {
        [Header("UI")] public GameObject ui;
        public Image[] images;
        public Button[] buttons;

        [Header("Items")] 
        public ItemData[] datas;
        public int[] amounts;
        public bool[] looted;
        
        public static Looting instance { get; private set; }


        private void Awake()
        {
            instance = this;
            looted = new bool[buttons.Length];
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            for(int i = 0; i < images.Length; i++)
            {
                images[i].gameObject.SetActive(false);
            }
            ui.SetActive(false);
        }


        public void OpenLootingUI(ItemData[] datas, int[] amounts)
        {
            this.datas = datas;
            this.amounts = amounts;

            for (int i = 0; i < amounts.Length; i++)
            {
                if (looted[i] == false) { 
                    buttons[i].GetComponentInChildren<Text>().text = "Take " + datas[i].name;
                    images[i].sprite = datas[i].icon;                
                    
                    buttons[i].gameObject.SetActive(true);
                    images[i].gameObject.SetActive(true);
                    images[i].preserveAspect = true;
                }
            }
            ui.SetActive(true);
        }

        public void TakeItem(int index)
        {
            Player.Player.main.inventory.entityInventory.AddItem(datas[index], amounts[index]);
            Debug.Log("Took item " + datas[index].name);
            buttons[index].gameObject.SetActive(false);
            images[index].gameObject.SetActive(false);
            looted[index] = true;

            List<Button> activeButtons = new List<Button>();
            
            for(int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].IsActive()) activeButtons.Add(buttons[i]);
                else activeButtons.Remove(buttons[i]);
                if (activeButtons.Count == 0)
                {
                    Debug.Log("Enemy is looted!");
                    //enemy.looted = true;
                }
            }
            
        }

        public void Back()
        {
            ui.SetActive(false);
        }
    }
}