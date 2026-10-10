using System;
using System.Collections.Generic;
using Scripts.Player.Movement;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Scripts.Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        public AbilitiesUI ui;
        
        [SerializeField] private Ability[] abilities; // if you want to manage something in abilities, use abilitiesObj
        
        private Player.Player player;

        public static AbilityManager instance { get; private set; }
        
        [HideInInspector] public List<Ability> abilitiesObj = new List<Ability>();

        public Light2D abilityLight;
        

        public void ShowUI()
        {
            ui.gameObject.SetActive(true);
        }

        public void HideUI()
        {
            ui.gameObject.SetActive(false);
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            player = Player.Player.main;
            PlayerController.instance.controls.Player.Ability1.performed += ctx => ActivateAbility(0);
            PlayerController.instance.controls.Player.Ability2.performed += ctx => ActivateAbility(1);
            PlayerController.instance.controls.Player.Ability4.performed += ctx => ActivateAbility(3);
            PlayerController.instance.controls.Player.Ability5.performed += ctx => ActivateAbility(4);

            for (int i = 0; i < abilities.Length; i++)
            {
                Ability ability = Instantiate(abilities[i]);
              //  ability.isUnlocked = false;
                abilitiesObj.Add(ability);
            }

            ShowUI();
            ui.UpdateUI(abilitiesObj);

        }

        private void Update()
        {
            for (int i = 0; i < abilitiesObj.Count; i++)
            {
                if (abilitiesObj[i].state == AbilityState.Activated)
                {
                    if (abilitiesObj[i].activeTime > 0)
                    {
                        Debug.Log("Usuwam activityTime\nAbility State: " + abilitiesObj[i].state);
                        abilitiesObj[i].activeTime -= Time.deltaTime;

                        if (abilitiesObj[i].checkWhileActivation)
                        {
                            abilitiesObj[i].WhileActivation();
                        }
                    }
                    else
                    {
                        Debug.Log("Ustawiam status na cooldown");
                        abilities[i].BeginCooldown();
                        abilitiesObj[i].state = AbilityState.Cooldown;
                        abilitiesObj[i].cooldownTime = abilitiesObj[i].maxCooldownTime;
                        ui.SetUICooldown(i, abilitiesObj[i].maxCooldownTime, abilitiesObj[i].maxCooldownTime);
                    }
                }

                if (abilitiesObj[i].state == AbilityState.Cooldown)
                {
                    if (abilitiesObj[i].cooldownTime > 0)
                    {
                        Debug.Log("Usuwam cooldown");
                        abilitiesObj[i].cooldownTime -= Time.deltaTime;
                        ui.SetUICooldown(i, abilitiesObj[i].cooldownTime, abilitiesObj[i].maxCooldownTime);
                    }
                    else
                    {
                        Debug.Log("Ability gotowa");
                        abilitiesObj[i].state = AbilityState.Ready;
                    }
                }
            }
            
        }

        public void ActivateAbility(int index)
        {
            if(!abilitiesObj[index].isUnlocked) return;
            Debug.Log("xdddd");
            switch (abilitiesObj[index].state)
            {
                case AbilityState.Ready:
                    abilitiesObj[index].Activate();
                    abilitiesObj[index].activeTime = abilitiesObj[index].maxActiveTime;
                    abilitiesObj[index].state = AbilityState.Activated;
                    break;
            }
        }
    }
}