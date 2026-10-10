using Scripts.Enemy;
using UnityEngine;
using Scripts.Player;

namespace Scripts.Abilities
{
    [CreateAssetMenu(fileName = "Blindness Ability", menuName = "CubeLand Studio/Abilities/New Blindness Ability")]
    public class BlindnessAbility : Ability
    {
        [Header("Blindness")] 
        public float radius;

        public float targetValue;
        
        public void IncreaseLightning()
        {
            Player.Player.main.anim.SetBool("LightAbility", true);
            LeanTween.value(Player.PlayerObjects.instance.abilityLightning.gameObject,
                    Player.PlayerObjects.instance.abilityLightning.intensity, targetValue, maxActiveTime).setEase(LeanTweenType.easeOutQuad).setOnUpdate(
                    (float value) =>
                    {
                        Player.PlayerObjects.instance.abilityLightning.intensity = value;
                    })
                .setOnComplete(() =>
                {   
                    DecreaseLightning();
                });
        }

        public void DecreaseLightning()
        {
            Player.Player.main.anim.SetBool("LightAbility", false);
            LeanTween.value(Player.PlayerObjects.instance.abilityLightning.gameObject,
                    targetValue, 0, maxActiveTime).setEase(LeanTweenType.easeOutQuad).setOnUpdate(
                    (float value) =>
                    {
                        Player.PlayerObjects.instance.abilityLightning.intensity = value;
                    })
                .setOnComplete(() =>
                {
                    
                });
        }
        
        public override void Activate()
        {
            IncreaseLightning();
            
            
            Collider2D[] hits = Physics2D.OverlapCircleAll(Player.Player.main.transform.position, radius,
                LayerMasks.instance.enemyMask);

            foreach (Collider2D hit in hits)
            {
                Mobs.Enemy e = hit.GetComponent<Mobs.Enemy>();
                e.DoBlind();
            }
            
            /*
            foreach (Collider2D hit in hits)
            {
                FightingAI enemy = hit.GetComponent<FightingAI>();
                if (enemy != null)
                {
                    Debug.Log("Znalazlem enemy");
                    IncreaseLightning();
                    // todo blindness stars;

                    enemy.isStunned = true;
                    PlayerCombat combat = Player.Player.main.GetComponent<PlayerCombat>();
                    combat.hittedEnemy.Add(enemy);
                }
                
            }
            */
        }

        public override void BeginCooldown()
        {
            PlayerCombat combat = Player.Player.main.GetComponent<PlayerCombat>();
           /* for (int i = 0; i < combat.hittedEnemy.Count; i++)
            {
                combat.hittedEnemy[i].isStunned = false;
                combat.hittedEnemy.Remove(combat.hittedEnemy[i]);
            }
            */
        }
    }
}