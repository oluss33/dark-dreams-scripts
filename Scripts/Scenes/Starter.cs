using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Inventory;
using Scripts.Player;
using UnityEngine;


namespace Scripts.Scenes
{
    public class Starter : MonoBehaviour
    {
        
        //***** Items *****\\
        private List<Item> itemsToGive;
        private List<Item> equippedItemsList;
        private EntityInventory inventory;
        private List<PlayerSkills.SkillType> unlockedSkillTypes;
        
        //***** Player Stats *****\\
        private float currentHp;
        private float maxHp;
        private float currentStamina;
        private float maxStamina;
        private float staminaRegenerationSpeed;
        private int protection;
        
        private float walkSpeed;
        private float runSpeed;
        private float crouchSpeed;
        
        private float exp;

        public void SetupPlayerStats(float currentHp, float maxHp, float currentStamina, float maxStamina,
            float staminaRegenerationSpeed, int protection, float walkSpeed, float runSpeed, float crouchSpeed,
            float exp)
        {
            this.currentHp = currentHp;
            this.maxHp = maxHp;
            this.currentStamina = currentStamina;
            this.maxStamina = maxStamina;
            this.staminaRegenerationSpeed = staminaRegenerationSpeed;
            this.protection = protection;
            this.walkSpeed = walkSpeed;
            this.runSpeed = runSpeed;
            this.crouchSpeed = crouchSpeed;
            this.exp = exp;
        }

        public void UnlockSkills(List<PlayerSkills.SkillType> unlockedSkillTypes)
        {
            this.unlockedSkillTypes = unlockedSkillTypes;
        }

        public void SetupItems(List<Item> itemsToGive, List<Item> equippedItemsList)
        {
            this.itemsToGive = itemsToGive;
            this.equippedItemsList = equippedItemsList;
        }

        private void Start()
        {
            Player.Player p = Player.Player.main;
            //********* Player Stats *********\\
            PlayerStats pS = PlayerStats.instance;
            PlayerHealth pH = PlayerHealth.instance;
            ;
            pS.maxHp = maxHp;
            pH.currentHp = currentHp;
            pS.maxStamina = maxStamina;
            pS.currentStamina = currentStamina;
            pS.staminaRegenerationSpeed = staminaRegenerationSpeed;
            pS.protection = protection;
            pS.walkSpeed = walkSpeed;
            pS.runSpeed = runSpeed;
            pS.crouchSpeed = crouchSpeed;
            
            //********* Items ***********\\
            if (itemsToGive.Count > 0)
            {
                inventory = p.inventory.entityInventory;
                inventory.SetupInventory(itemsToGive);
            }

            //********* Equipped Items ***********\\

            if (equippedItemsList.Count > 0)
            {
           //     inventory = p.equipmentInventory.entityInventory;
                // TODO: Finish this shit
            }

            //********* PlayerSkills ***********\\
            if (unlockedSkillTypes.Count > 0)
            {
                PlayerSkills skills = p.GetPlayerSkills();
                skills.unlockedSkillTypes = unlockedSkillTypes;

                foreach (PlayerSkills.SkillType s in unlockedSkillTypes)
                {
                    skills.UnlockSkill(s);
                }
                
            }
            
        }
    }
}