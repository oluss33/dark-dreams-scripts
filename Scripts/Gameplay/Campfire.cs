using System;
using Scripts.Inventory;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Gameplay
{
    public class Campfire : IInteractable
    {
        [SerializeField] private GameObject fire;
        [SerializeField] private float maxBurningTime;
        [SerializeField] private float time;

        private bool isBurning = false;
        
        private Player.Player p = Player.Player.main;
        private EntityInventory inv;
        
        private Interactable interactable;
        
        private void Start()
        {
            inv  = p.inventory.entityInventory;
            interactable = GetComponent<Interactable>();
            interactable.onPlayerOverlap += Test;
        }

        private void Test()
        {
            Hints.instance.ShowHint(gameObject, "Campfire", "Press E to Light the campfire and regenerate health over time.", 2);
        }


        private void Update()
        {
            if (isBurning)
            {
                time -= 1 * Time.deltaTime;

                var col = Physics2D.OverlapCircle(transform.position, 10, LayerMasks.instance.playerMask);

                if (col != null)
                {
                    PlayerHealth.instance.AddHp(1);
                }
            }

            if (time <= 0) { 
                time = 0; 
                if (isBurning) { 
                    TurnOffFire();
                } 
            } 
        }

        /*private void OnMouseDown()
        {
            if (isBurning) return;
            ItemData i = ItemsList.instance.torch;
            if (inv.inventory.HasItem(i))
            {
                LightFire();
                inv.RemoveItem(i, 1);
            }
        }*/

        void LightFire()
        {
            isBurning = true;
            fire.SetActive(true);
            time = maxBurningTime;
        }

        void TurnOffFire()
        {
            isBurning = false;
            fire.SetActive(false);
        }

        public override void Interact()
        {
            if (isBurning) return;
            LightFire();
        }

    }
}