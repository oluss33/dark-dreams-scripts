using System;
using Combat.Boss;
using Scripts.CameraExtras;
using Scripts.Combat.Player;
using Scripts.Enemy;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Combat.Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject parent; 
        
        [SerializeField] private float attackPower;
        [SerializeField] private float hitPower;
        public bool canKnockDown;
        public Stun stun;

        public bool isBoss;

        private Attack playerAttack;

        public GameObject playerParry;

        public bool activated = false;

        private Mobs.Enemy enemy;
        
        public bool performedStun = false;
        
        private void Start()
        {
            playerAttack = Scripts.Player.Player.main.GetComponent<Attack>();
            enemy = parent.GetComponent<Mobs.Enemy>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            print(this.gameObject.name + " kurwa XDDd");
            Debug.Log(other.gameObject.name);
            if (other.transform.root != transform.root)
            {
                if (activated) return;
                if (other.gameObject.name == "parry")
                {
                    Debug.Log("Stun registered parry attempt");
                    if (stun.parryWindow)
                    {

                        if(performedStun) return;
                        if(isBoss) transform.parent.gameObject.GetComponent<IBoss>().StunSound();
                        
                        //stun.StunMyself();
                        
                        PlayerStats.instance.currentStamina += 50;
                        PlayerStats.instance.cooldown = 0;
                        Debug.Log("Parry activated");
                        stun.onStunActivated?.Invoke();

                        print("ustawiam stunned enemyt");
                        playerAttack.stunnedEnemy = stun;
                        print(playerAttack.stunnedEnemy);  
                        performedStun = true;
                        
                        gameObject.SetActive(false);
                     //   parent.GetComponent<Animator>().SetTrigger("stun");
                      //  parent.GetComponent<EnemyAttackStatus>().stun = true;
                      //  GetComponent<EnemyWeapon>().enabled = false;
                    }
                }
                else if (other.gameObject.CompareTag(Tags.Player))
                {
                    if (/*!attack.attacked &&*/ !stun.activated && !playerParry.GetComponent<Collider2D>().enabled)
                    {
                       // attack.attacked = true;
                        Debug.Log("Hit " + other.gameObject.name);
                        
                        enemy.ShakeCamera();

                        print("zadaje graczu obrazenia. status stuna: " + stun.activated);

                        
                        var dir = transform.right;
                        if(!isBoss)
                            if (parent.GetComponent<AI>().mirrored_rotation)
                                dir = -transform.right;

                        
                        dir *= Math.Sign(transform.localScale.x);
                        PlayerHealth.instance.DealDamage(attackPower, dir);
                        if (canKnockDown)
                        {
                            Scripts.Player.Player.main.KnockDown();
                        }
                        activated = true;

                    }
                    if (gameObject.name == "visceral")
                    {
                        other.gameObject.GetComponent<Animator>().SetTrigger("breakStun");
                    }
                    
                }

                if (other.gameObject.tag == "Block")
                {
                    other.transform.root.GetComponent<PlayerHealth>().DecreaseStamina(hitPower);
                    activated = true;

                }

                if (other.gameObject.name == "dodge")
                {
                    gameObject.transform.root.GetComponent<Stun>().StunMyself();
                    activated = true;
                }
            }
        }
    }
}