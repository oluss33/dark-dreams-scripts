using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Scripts.Inventory
{
    public class Crafting : MonoBehaviour
    {
        public static Crafting instance { get; private set; }
        
        private Dictionary<string, Item[]> recipes;
        private List<string> recipeNames;

        public Transform t;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            // name, required items[l-1], produced item[l]
            recipes = new Dictionary<string, Item[]>();
            
            recipes.Add("Magic Sword", new Item[] { new Item(ItemsList.instance.sword, 1), new Item(ItemsList.instance.stick, 1) });

            foreach (var Item in GetRequiredItems("Magic Sword"))
            {
                print(Item.data.name);
            }
            
            print(GetProduct("Magic Sword").data.name);

            recipeNames = new List<string>();
            
            foreach(KeyValuePair<string, Item[]> recipe in recipes)
            {
                recipeNames.Add(recipe.Key);
            }
        }


        private void Update()
        {
            UpdateButtons();
        }

        public void Craft(string recipeName)
        {
            List<Item> requiredItems = GetRequiredItems(recipeName);
            Item product = GetProduct(recipeName);

            foreach (Item item in requiredItems)
            {
                Player.Player.main.inventory.entityInventory.RemoveItem(item.data, item.amount);
            }
            
            Player.Player.main.inventory.entityInventory.AddItem(product.data, product.amount);
        }

        void UpdateButtons()
        {
            foreach(string recipeName in recipeNames)
            {
                if (HasRequiredItems(recipeName))
                {
                    t.Find("Craft " + recipeName).GetComponent<Button>().interactable = true;
                }
                else
                {
                    t.Find("Craft " + recipeName).GetComponent<Button>().interactable = false;
                }
            }
        }
        
        
        bool HasRequiredItems(string recipeName)
        {
            List<Item> requiredItems = GetRequiredItems(recipeName);
            
            foreach (Item item in requiredItems)
            {
                if (!Player.Player.main.inventory.entityInventory.inventory.HasItem(item.data))
                {
                    return false;
                }

                if (Player.Player.main.inventory.entityInventory.inventory.HasItem(item.data))
                {
                    for (int i = 0; i < Player.Player.main.inventory.entityInventory.inventory.itemsList.Count; i++)
                    {
                        if (Player.Player.main.inventory.entityInventory.inventory.itemsList[i].data.name == item.data.name)
                        {
                            if (Player.Player.main.inventory.entityInventory.inventory.itemsList[i].amount < item.amount)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public List<Item> 
            
            GetRequiredItems(string recipeName)
        {

            List<Item> requiredItems = new List<Item>();
            foreach (KeyValuePair<string, Item[]> recipe in recipes)
            {
                if (recipe.Key == recipeName)
                {
                    // return recipe.Value;
                    for(int i = 0; i < recipe.Value.Length - 1; i++)
                    {
                        requiredItems.Add(recipe.Value[i]);
                    }
                    return requiredItems;

                }
            }

            return null;
        }
        
        Item GetProduct(string recipeName)
        {
            foreach (KeyValuePair<string, Item[]> recipe in recipes)
            {
                if (recipe.Key == recipeName)
                {
                    Item product = recipe.Value[recipe.Value.Length - 1];
                    return product;
                }
            }

            return new Item();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}