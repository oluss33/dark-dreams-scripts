using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Inventory;
using Scripts.Inventory.ItemFunctions;
using UnityEngine;

namespace Scripts.Player
{
    /// <summary>
    /// The class is for child objects of player
    /// </summary>
    public class PlayerObjects : MonoBehaviour
    {
        public static PlayerObjects instance { get; private set; }

        public UnityEngine.Rendering.Universal.Light2D torchLight;
        public UnityEngine.Rendering.Universal.Light2D abilityLightning;


        public ItemDataHolder d;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            
        }
    }
}
