namespace Scripts.Inventory
{
    public class InventoryEvents
    {
        public enum InventoryEventType
        {
            Add,
            Remove,
            Equip
        }
        public delegate void OnInventoryUpdate(Inventory source, Item item, InventoryEventType type);

        public delegate void OnEquipmentUpdate(Inventory source, Item item, Slot slot, InventoryEventType type);
        
        public delegate void OnChestUpdate(Inventory source, Item item, InventoryEventType type);
    }
}