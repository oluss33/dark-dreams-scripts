using System.Collections.Generic;
using Scripts.Inventory.ItemFunctions.Items;

namespace Scripts.Inventory.ItemFunctions
{
    public static class ItemFunctionality
    {
        private static IItemFunction nullItemForCallback = new DoNothingItemFunction();
        private static Dictionary<int, IItemFunction> idToItemFunction;

        public static IItemFunction GetItemFunction(ItemData data)
        {
            return idToItemFunction.GetValueOrDefault(data.id, nullItemForCallback);
        }

        public static void Init()
        {
            idToItemFunction = new Dictionary<int, IItemFunction>();
            idToItemFunction[6] = new TorchItemFunction();
            idToItemFunction[1] = new SwordItemFunctions(1); // Normal sword
            idToItemFunction[3] = new KeyItemFunction();
            idToItemFunction[7] = new BowItemFunction(7); // Normal Bow
        }
    }
}