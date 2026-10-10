using UnityEngine;

namespace Scripts.Inventory.ItemFunctions.Items
{
    public class KeyItemFunction : IItemFunction
    {
        public void Use()
        {
            Debug.Log("I'm using a key");
            Collider2D col = Physics2D.OverlapCircle(Player.Player.main.transform.position, 10f,
                LayerMasks.instance.doorsMask);
            if (col != null)
            {
                col.enabled = false;
                Debug.Log("Doors has been opened");
            }
        }

        public void Equip()
        {
        }

        public void UnEquip()
        {
        }
    }
}