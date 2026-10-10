using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Inventory {
    public enum IHolder
    {
        Candle,
        Coin,
        Key,
        Skeleton_Hat,
        Spider_Heart,
        Stick,
        Sweet,
        Sword,
        Torch
    }
    
    public class ItemWorldSelector : MonoBehaviour
    {
        public static ItemWorldSelector instance { get; private set; }
        
        private ItemsList i = ItemsList.instance;

        public ItemData ConvertIHolder(IHolder h)
        {
            switch (h)
            {
                case IHolder.Candle:
                    return i.candle;
                case IHolder.Coin:
                    return i.candle;
                case IHolder.Key:
                    return i.key;
                case IHolder.Stick:
                    return i.stick;
                case IHolder.Sword:
                    return i.sword;
                case IHolder.Torch:
                    return i.torch;
            }

            return i.stick;
        }

        public IHolder ConvertIData(ItemData d)
        {
            switch (d.name)
            {
                case "Candle":
                    return IHolder.Candle;
                case "Coin":
                    return IHolder.Coin;
                case "Key":
                    return IHolder.Key;
                case "Stick":
                    return IHolder.Stick;
                case "Sword":
                    return IHolder.Sword;
                case "Torch":
                    return IHolder.Torch;
            }

            return IHolder.Coin;
        }
        private void Awake()
        {
            instance = this;
        }
    }
}
