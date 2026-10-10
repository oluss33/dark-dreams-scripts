using Scripts.Combat.Player;
using UnityEngine;

namespace Scripts.Inventory.ItemFunctions.Items
{
    public class BowItemFunction : IItemFunction
    {

        private int id;

         public BowItemFunction(int id)
        {
            this.id = id;
        }
        public void Equip()
        {
            ItemData data = ItemsList.instance.datas.Find(itemData => itemData.id == id);
            Debug.Log("wyciagam łuk");
//            Debug.Log("I have a " + data.name);
            Player.Player.main.GetComponent<Attack>().SetBow(new Item(data, 1));
        }

        public void UnEquip()
        {
            Debug.Log("I don't have a bow");
            Player.Player.main.GetComponent<Attack>().ResetBow();
        }

        public void Use()
        {
        }
    }
}