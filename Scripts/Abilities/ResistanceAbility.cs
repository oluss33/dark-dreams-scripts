using System.Collections;
using System.Collections.Generic;
using Scripts.Combat.Enemy;
using Scripts.Player;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;


namespace Scripts.Abilities {
    [CreateAssetMenu(fileName = "Resistance Ability", menuName = "CubeLand Studio/Abilities/New Resistance Ability")]
    public class ResistanceAbility : Ability
    {
        [Header("Resistance Ability")]
        public float dmg;
        public Color lightColor;
        public float intensityValue;
        
        public override void Activate()
        {
            Light2D light = AbilityManager.instance.abilityLight;
            light.color = lightColor;

            LeanTween.value(0, intensityValue, maxActiveTime).setOnUpdate((float v) =>
            {
                light.intensity = v;
            }).setOnComplete(Resistance);
        }

        public void Resistance()
        {
            PlayerStats stats = PlayerStats.instance;
            stats.isPlayerResilient = true;
        }

        public override void BeginCooldown()
        {
            Light2D light = AbilityManager.instance.abilityLight;
            light.color = lightColor;
            
            LeanTween.value(intensityValue, 0, 1.5f).setOnUpdate((float v) =>
            {
                light.intensity = v;
            }).setOnComplete(() => Debug.Log("@Light_Ability: completed :)"));
            
            
            PlayerStats stats = PlayerStats.instance;
            stats.isPlayerResilient = false;
        }
    }
}
