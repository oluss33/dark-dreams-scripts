using UnityEngine;

namespace Scripts.Inventory
{
    public abstract class InventoryUI : MonoBehaviour
    {
        public abstract void InitializeUI();
        public abstract void Show();
        public abstract void Hide();
        public abstract bool isShown();
        public abstract void OnItemAdded(Item item);
        public abstract void OnItemRemoved(Item item);
    }
}