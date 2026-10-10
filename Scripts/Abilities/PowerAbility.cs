using System;
using Scripts.Combat.Player;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Abilities
{
    [CreateAssetMenu(fileName = "Power Ability", menuName = "CubeLand Studio/Abilities/New Power Ability")]
    public class PowerAbility : Ability
    {
        public float multiplier = 1.25f;

        public override void Activate()
        {
            PlayerStats stats = PlayerStats.instance;

            Attack _attack = Player.Player.main.GetComponent<Attack>();
            var _weapon = _attack.GetWeapon();

            _weapon.data.dmg *= multiplier;
            _attack.SetWeapon(_weapon);

            Debug.Log(_attack.GetWeapon().data.dmg);

            Animator anim = Player.Player.main.GetComponent<Animator>();

            stats.staminaRegenerationSpeed *= multiplier;
            
            anim.SetFloat("animSpeed", multiplier);
        }

        public override void WhileActivation()
        {
            PlayerHealth hp = Player.Player.main.GetComponent<PlayerHealth>();
            PlayerStats stats = PlayerStats.instance;

            if (hp.currentHp <= stats.maxHp)
            {
                hp.currentHp += 0.25f * Time.deltaTime;
            }

            if (hp.currentHp >= stats.maxHp)
            {
                hp.currentHp = stats.maxHp;
            }
        }

        public override void BeginCooldown()
        {
            PlayerStats stats = PlayerStats.instance;

            Attack _attack = Player.Player.main.GetComponent<Attack>();

            var _weapon = _attack.GetWeapon();

            _weapon.data.dmg /= multiplier;
            _attack.SetWeapon(_weapon);
            
            Animator anim = Player.Player.main.GetComponent<Animator>();
            
            anim.SetFloat("animSpeed", 1);
            
            stats.staminaRegenerationSpeed /= multiplier;
            
            Debug.Log(_attack.GetWeapon().data.dmg);
        }
    }
}