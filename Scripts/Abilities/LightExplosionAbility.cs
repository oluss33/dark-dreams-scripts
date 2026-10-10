using Scripts.Abilities.Functionality;
using Scripts.Combat.Enemy;
using Scripts.Combat.Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;


namespace Scripts.Abilities
{
    [CreateAssetMenu(fileName = "Light Explosion Ability", menuName = "CubeLand Studio/Abilities/New Light Explosion Ability")]
    public class LightExplosionAbility : Ability
    {
        [Header("Light Ability")]
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
            }).setOnComplete(TakeDamage);
        }

        public void TakeDamage()
        {
            Light2D light = AbilityManager.instance.abilityLight;

            // sphere
            Collider2D[] targets = Physics2D.OverlapCircleAll(light.gameObject.transform.position,
                light.pointLightOuterRadius, LayerMasks.instance.enemyMask);
            // take damage to all of the enemies
            foreach (Collider2D c in targets)
            {
                EnemyHealth e = c.GetComponent<EnemyHealth>();
                e.DealDamage(dmg);
                Debug.Log("@Light_Ability: I've hit an enemy");
            }
            // slowly decrease the light intensity 
            LeanTween.value(intensityValue, 0, 1.5f).setOnUpdate((float v) =>
            {
                light.intensity = v;
            }).setOnComplete(() => Debug.Log("@Light_Ability: completed :)"));
            // :) 
        }
    }
}