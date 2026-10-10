using UnityEngine;

namespace Scripts.Inventory
{
    [CreateAssetMenu(menuName = "CubeLand Studio/ItemData/New Item Data", fileName = "New Item Data")]
    public class ItemDataHolder : ScriptableObject
    {
        public ItemData data;
        
        public void UseItem()
        {
            if (data.name != null && data.onUse != null)
            {
                data.onUse.Invoke();
            }
            else
            {
                Debug.LogError("ItemData or onUse not properly initialized.");
            }
        }
    }
}