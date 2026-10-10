using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Combat;
using Scripts.Combat.Enemy;
using Scripts.Player;
using Scripts.Player.Movement;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Enemy
{
    public class AI : MonoBehaviour
    {
        [Header("Config")] [SerializeField] protected Animator anim;
        public float minDistance;
        public float raycastDistance;
        public float maxCooldown;
        public bool canDefend;
        public Vector3 offset; //= new Vector3(10, 0, 0); // for broken models
        [SerializeField] protected float currentCooldown;
        [SerializeField] protected float moveSpeed;
        [Header("Status")] public float stamina;
        public bool staminaBreak;
        public bool isUsingStamina;
        [SerializeField] public bool isAttacking;

        [SerializeField] protected GameObject player;

        protected AudioSource source;
        
        //[SerializeField] protected float staminaRegenerationSpeed;
        //[SerializeField] protected float minRequiredStamina;
        //public TextMeshProUGUI staminaText;

        public Stun stun;
        public EnemyWeapon weapon;

        public Action onAttack;

        protected float scale;

        public bool mirrored_rotation = false;

        public bool blockBaseMovement;

        private Vector2 startPos;
        
        public void ResetAI()
        {
            isAttacking = false;
            anim.Rebind();
            anim.Update(0);
            currentCooldown = 0;
            transform.position = startPos;
            anim.Play("static");
        }

        protected virtual void Start()
        {
            scale = transform.localScale.x;
            source = GetComponent<AudioSource>();
            startPos = transform.position;
        }

        protected virtual void Update()
        {
            LookForPlayer();
            CheckVariables();
            BaseAI();
        }

      /*  protected void ManageStun()
        {
            if (stun.activated)
            {
                currentCooldown = maxCooldown;
                stun.activated = false;
                weapon.enabled = true;
                Debug.Log("ustawiam cooldown, wlaczam bron");
            }
        }
*/

        public bool isCurrentlyAttacking = false;

        protected void LookForPlayer()
        {
            Collider2D coll =
                Physics2D.OverlapCircle(transform.position + offset, raycastDistance, LayerMasks.instance.playerMask);

            if (coll != null)
            {
                player = Player.Player.main.gameObject;

                if (player != null)
                {
                    isAttacking = true;
                }
            }
            else
            {
                isAttacking = false;
                anim.SetFloat("moveSpeed", 0);
            }
        }

        protected void CheckVariables()
        {
            /*  if (stamina < 100 && !isUsingStamina)
              {
                  stamina += staminaRegenerationSpeed * Time.deltaTime;
              }*/


            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }

            if (currentCooldown < 0)
            {
                currentCooldown = 0;
            }
        }

        protected void OnStunActivated()
        {
            stun.StunMyself();
            weapon.enabled = false;

            foreach (var param in anim.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Trigger)
                {
                    if (param.name == "Stun")
                    {
                        continue;
                    }
                    anim.ResetTrigger(param.name);
                }

                if (param.type == AnimatorControllerParameterType.Bool)
                {
                    if (param.name == "stun") return;
                    anim.SetBool(param.name, false);
                }

                if (param.type == AnimatorControllerParameterType.Float)
                {
                    anim.SetFloat(param.name, 0);
                }
            }
        }

        protected void BaseAI()
        {
            if (blockBaseMovement) return;
            if (PlayerHealth.instance.isDead) return;
            if(GetComponent<KnockBack>().IsBeingKnockBacked) return;
            if (isAttacking)
            {
                RotateNPC();
                if (Vector3.Distance(player.transform.position, transform.position) > minDistance /*&& !isUsingStamina*/
                   )
                {
                    Debug.Log("ruszam się");
                    var targetPos = new Vector2(player.transform.position.x, transform.position.y);
                    transform.position = Vector2.MoveTowards(transform.position, targetPos,
                        moveSpeed * Time.deltaTime);
                    // transform.Translate(new Vector2(transform.localScale.x, 0) * moveSpeed * Time.deltaTime);
                    if (anim != null)
                    {
                        anim?.SetFloat("moveSpeed", 1);
                    }

                    currentCooldown = 0;
                }
                else
                {
                    if (anim != null)
                    {
                        anim?.SetFloat("moveSpeed", 0);
                    }

                    if (canDefend)
                    {
                        if (currentCooldown == 0 && !staminaBreak)
                        {
                            Debug.Log("atakuje");
                            //anim.SetTrigger("isFighting");
                            InvokeAttack();
                            // stamina -= minRequiredStamina;
                            //currentCooldown = maxCooldown;
                            //stamina -= minRequiredStamina;*/
                        }
                    }
                }
            }
        }
        
        private void InvokeAttack() {
            onAttack?.Invoke();
            onAttack = null;
        }

        public void DisableAttack()
        {
            anim?.SetBool("isFighting", false);
        }

        protected void RotateNPC()
        {
            if (player == null)
                return;
            if (PlayerController.instance.performsDodge)
                return;

            var v = player.transform.position.x - transform.position.x;

            Debug.Log("rotate npc");
            
            
            // do pająka
            
            if ( v > 0 && transform.localScale.x < 0)
            {
                if (mirrored_rotation) transform.localScale = new Vector2(-scale, transform.localScale.y);
                else transform.localScale = new Vector2(scale, transform.localScale.y);
            }
            
            if (v < 0 && transform.localScale.x > 0)
            {
                if (mirrored_rotation) transform.localScale = new Vector2(scale, transform.localScale.y);
                else transform.localScale = new Vector2(-scale, transform.localScale.y);
            }
            
            if (v < 0 && transform.localScale.x < 0)
            {
                print("warunek 1");
                if (mirrored_rotation) transform.localScale = new Vector2(scale, transform.localScale.y);
                else transform.localScale = new Vector2(-scale, transform.localScale.y);
            }
            else if (v > 0 && transform.localScale.x > 0)
            {
                print("warunek 2");
                if (mirrored_rotation) transform.localScale = new Vector2(-scale, transform.localScale.y);
                else transform.localScale = new Vector2(scale, transform.localScale.y);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            if (raycastDistance == 0) return;
            Gizmos.DrawWireSphere(transform.position + offset, raycastDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + offset, minDistance);
        }
    }
}