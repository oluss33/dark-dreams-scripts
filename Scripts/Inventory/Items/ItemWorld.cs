using System;
using Scripts.Gameplay;
using Scripts.Player.Movement;
using Scripts.Tooltip;
using UnityEngine;

namespace Scripts.Inventory
{
    // TOOD: FIX: Player can't make collisions with "Item" layered objects. | 09.02.2025
    public class ItemWorld : IInteractable
    {
        public ItemDataHolder data;
        public int amount;
        public GameObject button;

        private BoxCollider2D col;

        private bool canPickup = false;
        [SerializeField] private float pickupDistance = 4f;


        public override void Interact()
        {
            PickUp();
            canPickup = false;
        }

        private void Start()
        {
            button.SetActive(false);
            ItemWorldSpawner.instance.Setup(data.data, col, gameObject);
        }

        void Update()
        {
            canPickup = Vector2.Distance(transform.position, Player.Player.main.transform.position) <= pickupDistance;
            if (canPickup)
            {
                KeyHints.instance.ShowInteractHint();
            }
            else
            {
                KeyHints.instance.HideInteractHint();
            }
        }

        private void PickUp()
        {
            KeyHints.instance.HideInteractHint();
            //if (interactionText != null)
                //interactionText.gameObject.SetActive(false);
            Debug.Log("You are trying to pickup item");
            if (data.data.equipable)
            {
                if (Player.Player.main.inventory.entityInventory.AddItem(data.data, amount) > 0)
                {
                    Debug.Log("You picked up item");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("You can't pickup this item!");
                    TooltipWarning.ShowTooltip_Static("You can't pickup this item!");
                }
            }
            else
            {
                if (Player.Player.main.inventory.entityInventory.AddItem(data.data, amount) > 0)
                {
                    Debug.Log("You picked up item");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("You can't pickup this item!");
                    TooltipWarning.ShowTooltip_Static("You can't pickup this item!");
                }
            }

        }
    }
}