using System;
using Scripts.Inventory;
using Scripts.Player.Movement;
using UnityEngine;

namespace Scripts.Combat.Player
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer weapon;
        [SerializeField] private SpriteRenderer bow;
        [SerializeField] private Attack attack;
        [SerializeField] private bool isHoldingWeapon;
        [SerializeField] private bool isHoldingBow;
        
        private PlayerControls controls;
        private PlayerWeapon pWeapon;
        
        private void Start()
        {
            controls = PlayerController.instance.controls;
            controls.Player.Weapon.performed += ctx => WeaponManagement();
            controls.Player.Bow.performed += ctx => BowManagement();

            pWeapon = weapon.GetComponent<PlayerWeapon>();
            weapon.gameObject.SetActive(false);
            bow.gameObject.SetActive(false);
        }

        private void BowManagement()
        {
            ItemData data = attack.GetBow().data;
            if (isHoldingWeapon) { HideWeapon(); isHoldingWeapon = false; }
            if(!isHoldingBow)
            {
                if(attack.GetBow().amount == 0) Debug.Log("Nie mam łuku");
                else
                {
                    Debug.Log("Wyciagam łuk " + data.id);
                    ShowBow(data);
                }
            }
            else
            {
                if(attack.GetBow().amount != 0)
                {
                    HideBow();
                }
            }

            isHoldingBow = !isHoldingBow;
        }

        private void WeaponManagement()
        {
            ItemData data = attack.GetWeapon().data;
            if(isHoldingBow) { HideBow(); isHoldingBow = false; }
            if (!isHoldingWeapon)
            {
                if (attack.GetWeapon().amount == 0) Debug.Log("Nie mam broni!");
                else
                {
                    Debug.Log("Wyciagam bron: " + data.name);
                    ShowWeapon(data);
                }
            }
            else
            {
                if (attack.GetWeapon().amount != 0)
                {
                    HideWeapon();
                }
            }
            
            isHoldingWeapon = !isHoldingWeapon;
        }

        private void HideWeapon()
        {
            weapon.gameObject.SetActive(false);
        }

        public void HideBow()
        {
            bow.gameObject.SetActive(false);
        }

        public void ShowBow(ItemData data)
        {
            bow.sprite = data.icon;
            bow.gameObject.SetActive(true);
        }

        private void ShowWeapon(ItemData data)
        {
            weapon.sprite = data.icon;
            weapon.gameObject.SetActive(true);
           
            pWeapon.PlayDrawClip();
            
            // TODO: Make weapon size
        }
    }
}