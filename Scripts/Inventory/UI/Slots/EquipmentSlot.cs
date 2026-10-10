namespace Scripts.Inventory
{
    public class EquipmentSlot : Slot
    {
        public enum SegmentType { Head, Neck, Body, Hand, Legs }

        public SegmentType segmentType;
    }
}