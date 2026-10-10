using System.Collections;
using UnityEngine;

namespace Scripts.Inventory
{
    public enum ItemType { Normal, Head, Body, Legs, Neck, Hand }
    
    [System.Serializable]
    public struct ItemData 
    {
        public string name;
        public string description;
        public int maxStack;
        public int id;
        public Sprite icon;
        public bool equipable;
        public OnUse onUse;
        public OnEquip onEquip;
        public OnUnEquip onUnEquip;
        public bool isUsable;
        public ItemType type;
        public int cost;
        public float dmg;
        public float cooldown;

        public delegate void OnUse();

        public delegate void OnEquip();

        public delegate void OnUnEquip();

        public ItemData(string name, string description, int maxStack, int id, Sprite icon, bool equipable, OnUse onUse, OnEquip onEquip, OnUnEquip onUnEquip, bool isEquipped, bool isUsable, ItemType type, int cost, float dmg, float cooldown)
        {
            this.name = name;
            this.description = description;
            this.maxStack = maxStack;
            this.id = id;
            this.icon = icon;
            this.equipable = equipable;
            this.onUse = onUse;
            this.onEquip = onEquip;
            this.onUnEquip = onUnEquip;
            this.isUsable = isUsable;
            this.type = type;
            this.cost = cost;
            this.dmg = dmg;
            this.cooldown = cooldown;
        }

        public int GetCost()
        {
            return cost;
        }
    }
}