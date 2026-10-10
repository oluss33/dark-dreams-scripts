using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Inventory
{
    public class CraftingButton: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private string recipeName;

        void Start()
        {
            recipeName = gameObject.name.Substring(6, gameObject.name.Length-6);
            print(recipeName);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            List<Item> requiredItems = Crafting.instance.GetRequiredItems(recipeName);

            List<string> strings = new List<string>();
            
            foreach(Item i in requiredItems)
            {
                string my_string = i.data.name + ": " + i.amount;
                strings.Add(my_string);
            }

            string final_string = "" + string.Join("\n", strings);
        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }
}