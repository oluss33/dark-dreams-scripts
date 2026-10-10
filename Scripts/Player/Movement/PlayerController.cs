using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Combat.Enemy;
using Scripts.Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Scripts.Player.Movement
{
    enum MovementType
    {
        Normal,
        Running
    }

    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance { get; private set; }
        public PlayerControls controls;
        public Rigidbody2D rb;
        public bool isGrounded;
        public float gravity;
        public bool dodge;

        [SerializeField] private LayerMask groundLayer;

        [Header("Ground")] 
        public Transform groundCheck;
        public float groundRadius;

        [Header("Dodge")] 
        /* TODO: Move to PlayerStats.cs */ 
        public float dodgeCooldown;
        public float maxDodgeCooldown;
        public bool performsDodge;

        [Header("Movement")]
        private bool isRunning;
        public bool isWalking = true;
        public float speedModifier = 1;
        
        public bool isJumping()
        {
            return currentMovement.shouldJump;
        }

        public bool IsRunning()
        {
            return isRunning;
        }
        
        public static Movement[] movements;

        public float jumpForce;
        
        private MovementType movementType = MovementType.Normal;

        private Movement currentMovement;

        private Movement targetMovement;

        private PlayerStats stats;
        private PlayerHealth health;

        private KnockBack knockback;
        [SerializeField] private float interactionDist;
        [SerializeField] private float lootingDist;

        private EnemyHealth closestLootableEnemy;

        private void Awake()
        {
            instance = this;

            controls = new PlayerControls();
            controls.Enable();

            controls.Player.Sprint.performed += ctx => PerformSprint();
            controls.Player.Sprint.canceled += ctx => isRunning = false;

            controls.Player.Interact.performed += ctx => Interact();
            controls.Player.Loot.performed += ctx => Loot();
        }

        private void Loot()
        {
            closestLootableEnemy?.LootEnemy();
        }
        
        private void Interact()
        {
            var interactables = Physics2D.OverlapCircleAll(transform.position, interactionDist, LayerMasks.instance.interactableMask);
            if (interactables != null)
            {
                foreach(var interactable in interactables) {
                    var interactableScript = interactable.GetComponents<IInteractable>();

                    print(interactable);
                    if (interactableScript != null)
                    {
                        foreach (var script in interactableScript)
                        {
                            script.Interact();
                        }
                    }
                }
            }
        }

        public void EnableControls()
        {
            controls.Enable();
        }

        public void DisableControls()
        {
            controls.Disable();
        }

        void PerformSprint()
        {
            if (stats.currentStamina > 0) isRunning = true;
            else isRunning = false;
        }

        public void PerformDodge()
        {
         //   anim.SetBool("DodgeLeft", false);
         //   anim.SetBool("DodgeRight", false);
            //transform.Translate(new Vector2(-2 * transform.localScale.x, 0));
        }

        

        private void Start()
        {
            movements = new Movement[]
            {
                new NormalMovement(22f, jumpForce),
                new NormalMovement(30f, jumpForce),
            };

            currentMovement = movements[0];

            foreach (Movement m in movements)
            {
                m.StartMovement(this);
            }

            rb = GetComponent<Rigidbody2D>();
            stats = PlayerStats.instance;
            health = PlayerHealth.instance;
            knockback = GetComponent<KnockBack>();
        }

        public Vector2 GetDirection()
        {
            return currentMovement.moveDirection;
        }

        public Movement GetMovement()
        {
            return currentMovement;
        }

        private void Update()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

            targetMovement = movements[(int)movementType];

            if (dodgeCooldown > 0) dodgeCooldown -= Time.deltaTime;
            if (dodgeCooldown < 0) dodgeCooldown = 0;

            if (knockback.IsBeingKnockBacked) return;
            
            
            if (rb.linearVelocity.x != 0)
            {
                isWalking = true;
            }
            
            if (isRunning && stats.currentStamina > 0 && rb.linearVelocity.x != 0)
            {
                health.isUsingStamina = true;
                health.DecreaseStamina(20);
                
                //health.staminaBar.transform.parent.gameObject.SetActive(true);
                
                movementType = MovementType.Running;
            }
            else
            {
                isRunning = false;
                health.isUsingStamina = false;
                
      //          health.staminaBar.transform.parent.gameObject.SetActive(false);

                
                movementType = MovementType.Normal;
            }

            if (currentMovement != targetMovement)
            {
                currentMovement = targetMovement;
            }

            if (currentMovement != null)
                currentMovement.Update();

            currentMovement.speedModifier = speedModifier;

            closestLootableEnemy = Physics2D.OverlapCircle(transform.position, lootingDist, LayerMasks.instance.enemyMask)?.GetComponent<EnemyHealth>();

            if (closestLootableEnemy != null && closestLootableEnemy.isDead)
            {
                KeyHints.instance.ShowLootHint();
            }
            else
            {
                KeyHints.instance.HideLootHint();
            }
        }

        void FixedUpdate()
        {
            if (currentMovement != null && !knockback.IsBeingKnockBacked)
                currentMovement.FixedUpdate();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
            Gizmos.color = Color.aliceBlue;
            Gizmos.DrawWireSphere(transform.position, interactionDist);
        }
    }
}