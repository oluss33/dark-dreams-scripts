using System.Collections;
using System.Collections.Generic;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Abilities
{
    
    public enum AbilityState
    {
        Ready,
        Activated,
        Cooldown
    }

    public class Ability : ScriptableObject
    {
        [Header("Base")] 
        public Sprite icon;
        public string abilityName;
        public PlayerSkills.SkillType skillType;
        public float cooldownTime;
        public float activeTime;
        public float maxCooldownTime;
        public float maxActiveTime;
        public bool isUnlocked = false;
        public AbilityState state;
        public bool checkWhileActivation = false;

        public virtual void Activate()
        {
            Debug.Log("I'm using ability!\nAbility Name: " + abilityName);
        }
        public virtual void BeginCooldown() { }
        public virtual void WhileActivation() { }
    }
}