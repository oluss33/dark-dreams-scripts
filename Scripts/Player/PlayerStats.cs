using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.Player
{
    [DefaultExecutionOrder(-5000)]

    public class PlayerStats : MonoBehaviour
    {
        
        public static PlayerStats instance { get; private set; }
        
        [Header("Health")]
        public float maxHp;
        public float currentStamina;
        public float maxStamina;
        public float staminaRegenerationSpeed = 20;
        public float currentEnergy;
        public float maxEnergy;
        public float energyRegenerationSpeed = 20;

        public float energyRegenDelayTime;
        public float energyRegenDelay;

        public float cooldown = 0;
        
        public int protection;

        [Header("Movement")] 
        public float walkSpeed;
        public float runSpeed;
        public float crouchSpeed;

        public float exp;
        
        [Header("UI")]
        public Text expTxt;


        [Header("Combat")] 
        public float strongAttackModifier;
        public float dodgeEnergyCost = 20;
        public float parryEnergyCost = 30;
        
        [Header("Abilities")]
        public bool secretsUnlocked;
        public bool isPlayerVisible;
        public bool isPlayerResilient;
        
        private void Awake()
        {
            if(instance == null) instance = this;
            else Debug.LogError("More than 1 PlayerStats instance!");
        }

        private void Start()
        {
            maxHp = 100;

            maxStamina = 100;
            currentStamina = maxStamina;
            
            expTxt.text = exp.ToString();
        }
        
        public void AddExp(float amount)
        {
            exp += amount;
            expTxt.text = exp.ToString();
        }

        public void DecreaseExp(float amount)
        {
            exp -= amount;
            expTxt.text = exp.ToString();

            if (exp <= 0)
            {
                exp = 0;
            }
        }

        public bool CheckExp(float amount)
        {
            return exp >= amount;
        }
    }
}