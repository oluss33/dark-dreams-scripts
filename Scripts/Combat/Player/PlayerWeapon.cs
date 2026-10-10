using System;
using Scripts.Enemy;
using Scripts.Player;
using UnityEngine;
using Scripts.Combat.Enemy;

namespace Scripts.Combat.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private AudioClip drawClip;
        [SerializeField] private AudioClip hitClip;
        [SerializeField] private AudioSource source;
       

        private Attack attack;

        public void PlayDrawClip()
        {
            source.PlayOneShot(drawClip);
        }
        
        public void PlayHitClip()
        {
            source.PlayOneShot(hitClip);
        }
        
        private void Start()
        {
            attack = transform.root.GetComponent<Attack>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.root != transform.root)
            {
                Debug.Log("Player hit");
                Debug.Log(col.gameObject.tag);
                if (col.gameObject.tag == "Enemy" && !attack.alreadyAttacked)
                {
                    Debug.Log("nakurwiam enemy XD");
                    attack.alreadyAttacked = true;
                    var enemy = col.gameObject.GetComponent<EnemyHealth>();
                    
                    var dmg = attack.GetWeapon().data.dmg;

                    if (attack.strongAttackPerformed)
                    {
                        dmg *= PlayerStats.instance.strongAttackModifier;
                    }
                    
                    enemy.DealDamage(attack.GetWeapon().data.dmg);

                    
                    Debug.Log("Player hit with " + attack.GetWeapon().data.dmg + " DMG");
                }
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            attack.alreadyAttacked = false;
        }
    }
}