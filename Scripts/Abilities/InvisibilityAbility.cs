using Scripts.Combat.Player;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Abilities
{
    [CreateAssetMenu(fileName = "Invisibility Ability", menuName = "CubeLand Studio/Abilities/New Invisibility Ability")]
    public class InvisibilityAbility : Ability
    {

        public Color visibleColor;
        public Color invisibleColor;
        
        public override void Activate()
        {
            Debug.Log("@Invisibility_Ability: activation");
            MakePlayerInvisible();
        }
        
        public override void WhileActivation()
        {
            if (Player.Player.main.GetComponent<Attack>().isFighting && !PlayerStats.instance.isPlayerVisible)
            {
                MakePlayerVisible();
                activeTime = 0;
            }
        }

        public override void BeginCooldown()
        {
            MakePlayerVisible();
        }

        void MakePlayerInvisible()
        {
            Player.PlayerStats stats = Player.PlayerStats.instance;
            stats.isPlayerVisible = false;
            
            LeanTween.value(visibleColor.a, invisibleColor.a, 1f).setOnUpdate((float v) =>
            {
                foreach (SpriteRenderer s in Player.Player.main.gameObject.GetComponentsInChildren<SpriteRenderer>())
                {
                    s.color = new Color(s.color.r, s.color.g, s.color.b, v);
                }
            });

        }

        void MakePlayerVisible()
        {
            Player.PlayerStats stats = Player.PlayerStats.instance;
            stats.isPlayerVisible = true;
            

            LeanTween.value(invisibleColor.a, visibleColor.a, 1f).setOnUpdate((float v) =>
            {
                foreach (SpriteRenderer s in Player.Player.main.gameObject.GetComponentsInChildren<SpriteRenderer>())
                {
                    s.color = new Color(s.color.r, s.color.g, s.color.b, v);
                }
            });
        }
    }
}