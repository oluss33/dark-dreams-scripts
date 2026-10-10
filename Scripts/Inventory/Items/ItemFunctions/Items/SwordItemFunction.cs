using Scripts.Combat.Player;
using UnityEngine;

namespace Scripts.Inventory.ItemFunctions.Items
{
    public class SwordItemFunctions : IItemFunction
    {
        private int id;
        
        public void Use() { }

        public SwordItemFunctions(int id)
        {
            this.id = id;
        }

        public void Equip()
        {
            ItemData data = ItemsList.instance.datas.Find(itemData => itemData.id == id);
//            Debug.Log("I have a " + data.name);
            Player.Player.main.GetComponent<Attack>().SetWeapon(new Item(data, 1));
        }

        public void UnEquip()
        {
            Debug.Log("I don't have a sword");
            Player.Player.main.GetComponent<Attack>().ResetWeapon();
        }
    }
}