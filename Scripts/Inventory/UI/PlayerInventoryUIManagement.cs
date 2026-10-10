using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Player.Movement;
using UnityEngine;

namespace Scripts.Inventory
{
    public class PlayerInventoryUIManagement : MonoBehaviour, Openable
    {
        [SerializeField] private GameObject UI;
        [SerializeField] private GameObject inventoryBlur;
        public bool isOpened;
        
        
        private void Start()
        {
            PlayerController.instance.controls.Player.Inventory.performed += ctx => ManageInventory();
        }

        void ManageInventory()
        {
            Openable[] openables =
                FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID)
                    .OfType<Openable>().ToArray();
            foreach (Openable openable in openables)
            {
                if (openable.IsOpened() && openable != this)
                {
                    return;
                }
            }
            
            isOpened = !isOpened;
            inventoryBlur.SetActive(isOpened);
            
            if (isOpened)
            {
                LeanTween.moveLocalX(UI, 800, 0.5f);
            }
            else
            {
                LeanTween.moveLocalX(UI, 0, 0.5f);
            }

        }

        public bool IsOpened()
        {
            return isOpened;
        }
    }
}