using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.CameraExtras;
using Scripts.Combat.Enemy;
using Scripts.Enemy;
using UnityEngine;
using Scripts.Player;
using Scripts.Combat.Player;
using Scripts.Management;
using Scripts.Player.Movement;

namespace Scripts.Combat
{
    public delegate void OnStunActivated();

    public class Stun : MonoBehaviour
    {
        public float parryDist;
        public bool parryWindow;

        public OnStunActivated onStunActivated;

        public bool activated = false;

        public GameObject parryIndicator;

        [SerializeField] private Transform parent;
        
        private Scripts.Player.Player p;

        private Attack playerAttack;
        private Mobs.Enemy enemy;
        
        private void Start()
        {
            p = Scripts.Player.Player.main;
            enemy = parent.GetComponent<Mobs.Enemy>();
            playerAttack = p.GetComponent<Attack>();
        }

        private void Update()
        {
            if(Vector2.Distance(transform.position, p.transform.position) <= parryDist) {
                if (!activated && parryWindow)
                {
                    parryIndicator.SetActive(true);
                    print("odpalam indikator");

                    if(PlayerController.instance.controls.Attack.Parry.triggered)
                    {
                        Debug.Log("Parry activated - STUN!!!!");
                        playerAttack.stunnedEnemy = this;


                        onStunActivated?.Invoke();
                    }

                }
                else if(!parryWindow) parryIndicator.SetActive(false);
            }
            else { parryIndicator.SetActive(false); /*print(gameObject.name + " wylaczam to gowno");*/}
        }

        public bool shaked = false;

        public void StunMyself()
        {
            if (shaked) return;
            TimeManager.instance.DoSlowMotion(0.3f, 1.25f);
            print("stun myself kurwa");
            //CinemachineEffects.instance.ShakeCamera(.3f, 1.25f);
            enemy.ShakeCamera();
            shaked = true;
            
            //activated = true;
            //print("stun acxtivated");

            /*if (gameObject.tag != Tags.Player)
            {
                //GetComponent<EnemyAttack>().cooldown = GetComponent<EnemyAttack>().maxCooldown;
                //  GetComponent<EnemyAttack>().timer = 0;
            }*/
        }

       /* private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Stun detected " + col.gameObject.name);
            if (col.gameObject.name == "parry")
            {
                Debug.Log("Stun registered parry attempt");
                if (parryWindow)
                {
                    PlayerStats.instance.currentStamina += 50;
                    PlayerStats.instance.cooldown = 0;
                    Debug.Log("Parry activated");

                    //GetComponent<EnemyAttackStatus>().stun = true;
                    //GetComponent<EnemyWeapon>().enabled = false;
                    //GetComponent<Animator>().SetTrigger("stun");

                    onStunActivated?.Invoke();

                }
            }
        }*/

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, parryDist);           
        }
    }
}