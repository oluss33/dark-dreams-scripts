using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Enemy;
using Scripts.Player.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private PlayerControls controls;
        Animator playerAnimator;
        PlayerStats stats;
        public TextMeshProUGUI staminaText;
        public bool isUsingStamina;
        void Awake()
        {
            controls = PlayerController.instance.controls;
            
            controls.Attack.Attack.performed += ctx => StartAttack();
            controls.Attack.Parry.performed += ctx => StartParry();
            playerAnimator = GetComponent<Animator>();
            stats = GetComponent<PlayerStats>();
        }

        // Update is called once per frame
        void Update()
        {
            /*if (playerHealth.currentStamina < 100 && !isUsingStamina)
            {
                playerHealth.currentStamina += 15 * Time.deltaTime;
            }*/
            //staminaText.text = "Player's Stamina: " + Mathf.Round(playerHealth.currentStamina).ToString() + "/100";
        }

        void StartAttack()
        {
            if (stats.currentStamina > 0)
            {
                playerAnimator.SetTrigger("isFighting");
                stats.currentStamina -= 25;
            }
            //else
            //{
            //    playerAnimator.SetTrigger("exhausted");
            //}
        }

        void StartParry()
        {
            if (stats.currentStamina > 0)
            {
                playerAnimator.SetTrigger("parry");
                stats.currentStamina -= 50;
            }
            //else
            //{
            //    playerAnimator.SetTrigger("exhausted");
            //}
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
        /*public Transform hitCheck;
        public float attackRange;
        public LayerMask enemyMask;
        
        public Image energyBar;
        public float currentEnergy;
        
        public float maxEnergy;
        public float attackPower;
        public float energyRegenerationSpeed;
        public float parryWindow;
        public float headDmg;
        public float bodyDmg;
        public float legsDmg;

        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            Player.main.playerInputActions.Player.Head.performed += ctx => Attack(true, false); 
            Player.main.playerInputActions.Player.Body.performed += ctx => Attack(false, true);
           // Player.main.playerInputActions.Player.Legs.performed += ctx => Attack(false, false, true);
            
            maxEnergy = 1;
            currentEnergy = maxEnergy;
            energyBar.fillAmount = currentEnergy;
        }

        private void Update()
        {
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += energyRegenerationSpeed * Time.deltaTime;
                energyBar.fillAmount = currentEnergy;
            }
            if (currentEnergy > maxEnergy)
            {
                currentEnergy = maxEnergy;
                energyBar.fillAmount = currentEnergy;
            }
        }

        void Attack(bool head, bool body)
        {
            
            // Searching for enemy
            Collider2D[] hits = Physics2D.OverlapCircleAll(hitCheck.position, attackRange, enemyMask);
            foreach (Collider2D hit in hits)
            {
                // Try to deal damage from enemy
                NewAI enemy = hit.GetComponent<NewAI>();
                
                if (enemy.cooldown < enemy.stunWindow / 2 || enemy.cooldown > enemy.stunWindow)
                {
                    if (hit.GetComponent<EnemyHealth>().isDead) return;
                    Debug.Log("You hit" + hit.name);
                    anim.SetTrigger("isFighting");
                    StartCoroutine(ShowCooldown(1));
                    currentEnergy = 0;
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {

                        Vector2 direction = hit.transform.position - transform.position;
                        direction.Normalize();

                        if (head)
                        {
                            enemyHealth.Head();
                        }
                        if (body)
                        {
                            enemyHealth.Body();
                        }

                        GetComponent<Rigidbody2D>().velocity = direction * 15f;
                        if (legs)
                        {
                            enemyHealth.Legs();
                        }
                    }
                }
            }
        }*/

      /*  IEnumerator ShowCooldown(float time)
        {
            GameManager.Instance.cooldownBar.SetActive(true);
            yield return new WaitForSeconds(time);
            GameManager.Instance.cooldownBar.SetActive(false);
        }*/

        /*private void OnDrawGizmosSelected()
        {
            if (hitCheck == null) return;
            Gizmos.DrawWireSphere(hitCheck.position, attackRange);
        }*/
    }
}
