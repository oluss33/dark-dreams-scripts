using System;
using System.Runtime.Remoting.Messaging;
using Scripts.Enemy;
using UnityEngine.UI;
using UnityEngine;

namespace Scripts.Combat.Enemy
{
    // DEPRECATED!! TODO: DELETE THIS SCRIPT IF EVERYTHING IS OK
    public class EnemyAttack : MonoBehaviour
    {
        private GameObject player;
        //bool isAttacking;
        [Header("Config")] public float minDistance;
        public float visibilityDistance;
        public float combatGap;
        public float cooldown;
        public float maxCooldown;
        public bool canDefend;
        public bool canAttack;
        public GameObject sliders;

        [Header("Status")] public float stamina;
        public bool staminaBreak;
        public bool isUsingStamina;
        public bool isTurningAround;
        float turnDelay = 0;

        [Header("UI")] [SerializeField] private bool isShowed;

        Animator animator;
        //private Animator playerAnimator;

        private float _localScale;

        private Mobs.Enemy enemy;





        
        public void TurnAround()
        {
            animator.SetBool("turnAround", false);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        

        private void Start()
        {
            animator = GetComponent<Animator>();
            // playerAnimator = player.GetComponent<Animator>();
            player = Scripts.Player.Player.main.gameObject;
            _localScale = transform.localScale.x;
            enemy = GetComponent<Mobs.Enemy>();
        }

        private void Update()
        {
            if (stamina < 100 && !isUsingStamina)
            {
                stamina += 10 * Time.deltaTime;
            }

            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                cooldown = 0;
            }

            /*
            if (canAttack)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            */


            if (Vector2.Distance(transform.position, player.transform.position) <= visibilityDistance)
            {
                //if (!isShowed)
                //{
                //    EnemyUI.instance.ShowEnemy(GetComponent<EnemyHealth>());
                //    isShowed = true;
                //}

                //                sliders.SetActive(true);
                //if (!isUsingStamina)
                // {
                if (player.transform.position.x > transform.position.x && transform.localScale.x < 0)
                {
                    //turnDelay += Time.deltaTime;
                    //if (turnDelay >= 2)
                    //{
                    //    Debug.Log("I'm turning around");
                    //    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    //    turnDelay = 0;
                    //}
                    //animator.SetTrigger("lookAround");
                    animator.SetBool("turnAround", true);
                }
                else if (player.transform.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    //turnDelay += Time.deltaTime;
                    //if (turnDelay >= 2)
                    //{
                    //    Debug.Log("I'm turning around");
                    //    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    //    turnDelay = 0;
                    //}
                    //animator.SetTrigger("lookAround");
                    animator.SetBool("turnAround", true);
                }
                else if (Vector3.Distance(player.transform.position, transform.position) > minDistance && !isUsingStamina)
                {
                    Debug.Log("@Spider: I'm moving to a player");
                    //transform.Translate(enemy.moveSpeed * Time.deltaTime * new Vector2(transform.localScale.x, 0));
                    animator?.SetFloat("Speed", 1);
                }
                else if (Vector2.Distance(player.transform.position, transform.position) <= minDistance)
                {
                    if (!isTurningAround)
                    {
                        animator?.SetFloat("Speed", 0);
                        if (canDefend && !isUsingStamina)
                        {
                            if (cooldown == 0)
                            {
                                Debug.Log("@Spider: I'm trying to attack a player");
                                animator?.SetTrigger("Attack");
                                cooldown = maxCooldown;
                            }
                        }
                    }
                }
            }
            else
            {
                /*if (isShowed)
                {
                    EnemyUI.instance.HideEnemy(GetComponent<EnemyHealth>());
                    isShowed = false;
                }*/
                //sliders.SetActive(false);
            }
        }

        public void SetCooldown(int targetValue)
        {
            cooldown = targetValue;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, minDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, visibilityDistance);
        }
    }
}