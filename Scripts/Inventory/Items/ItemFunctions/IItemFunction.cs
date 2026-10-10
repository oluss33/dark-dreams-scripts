namespace Scripts.Inventory.ItemFunctions
{
    public interface IItemFunction
    {
        void Use();
        void Equip();
        void UnEquip();
    }
}