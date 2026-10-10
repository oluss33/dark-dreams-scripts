using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using Scripts.Inventory;
using Scripts.Scenes;
using Scripts.Skills;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Scripts.Player.Movement;
using Scripts.Combat.Player;

namespace Scripts.Player
{
    [DefaultExecutionOrder(-10290)]
    public class Player : MonoBehaviour
    {
        public static Player main { get; private set; }
        [HideInInspector] public Animator anim;

        [SerializeField] public PlayerInventoryUI inventory;
        // [SerializeField] public PlayerInventoryUI equipmentInventory;
        
        [Header("Objects")]
        public Transform groundCheck;
        public Transform frontCheck;
        public Transform cameraTR;
        public Transform roofCheck;
        public Transform testTR;

        [SerializeField] UI_SkillTree skillTree;

        [Header("Variables")]
        [HideInInspector] public bool died = false;
        [HideInInspector] public bool grabbingLedge = false;
        [HideInInspector] public bool canRotate = true;
        [HideInInspector] public bool canRun = true;
        [HideInInspector] public bool isInteractionUnlocked = false;
        [HideInInspector] public int healthAddition = 0;
        [HideInInspector] public int lucky;
        public bool isFighting;

        [Header("Collisions")]
        public GameObject normalCol;
        public GameObject crouchingCol;

        [HideInInspector] public List<Quest.Quest> unlockedQuests = new List<Quest.Quest>();

        PlayerSkills playerSkills = new PlayerSkills();

        [HideInInspector] public List<ConceptArts.ConceptArt> unlockedConceptArts = new List<ConceptArts.ConceptArt>();

        [HideInInspector] public AudioSource source;
        [HideInInspector] public bool isTravelling = false;

        Rigidbody2D rb;
        public AudioClip[] punchClips;

        private PlayerController controller;
        private Attack attack;

        [SerializeField] float enemyRadius = 120f;

        private PlayerHealth health;
        private PlayerStats stats;

        void Awake()
        {
            if (main == null)
            {
                main = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            testTR = transform.Find("TestTR");
        }


        // TODO przeniesc do innego kodu ASAP
        void jebac(Scene scene, LoadSceneMode mode)
        {
            PlayerStats pS = PlayerStats.instance;
            GameObject go = Instantiate(new GameObject(), Vector3.zero, quaternion.identity);
            go.AddComponent<Starter>();
            
            go.GetComponent<Starter>().SetupItems(inventory.entityInventory.inventory.itemsList, inventory.entityInventory.inventory.equippedItemsList);
            go.GetComponent<Starter>().SetupPlayerStats(PlayerHealth.instance.currentHp, pS.maxHp, pS.currentStamina, pS.maxStamina,
                pS.staminaRegenerationSpeed, pS.protection, pS.walkSpeed, pS.runSpeed, pS.currentStamina, pS.exp);

        }

        private bool isOnRight;
        private bool isHigher;
        
        private float t = 0;
        private void Update()
        {
          /*  // debug
            // przeniesc do innego kodu TODO 
            // TODO: WYJEBAC CO TO KURWA JEST XD
            if (Input.GetKeyDown(KeyCode.X))
            {
                inventory.entityInventory.AddItem(ItemsList.instance.stick, 1);
                 playerInputActions.Disable();
                 SceneManager.sceneLoaded += jebac;
                 SceneManager.LoadScene("Swamps");
            }
            */
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, enemyRadius, LayerMasks.instance.enemyMask);

            /*if (enemies.Length > 0)
            {
                StartFight();
            }
            else
            {
                StopFight();
            }*/
        }

        void Start()
        {
            attack = GetComponent<Attack>();
            controller = GetComponent<PlayerController>();
            isInteractionUnlocked = false;
            health = PlayerHealth.instance;
            stats = PlayerStats.instance;

         //   playerInputActions = new PlayerInputActions();
         //   playerInputActions.Player.Enable();
         //   playerInputActions.Attack.Enable();

           // playerInputActions.Player.FPS.performed += ctx => GameManager.Instance.ManageFPS();

            playerSkills.OnSkillUnlocked += playerSkills.PlayerSkills_OnSkillUnlocked; // if didn't work, move this function from PlayerSkills.cs to Player.cs
            skillTree.SetPlayerSkills(playerSkills);


            source = GetComponent<AudioSource>();
            lucky = 0;

            controller.controls.Player.DodgeRight.performed += ctx => Dodge("r");
            controller.controls.Player.DodgeLeft.performed += ctx => Dodge("l");
        }

        private void Dodge(string s)
        {
            if (!isFighting) return;
            if(stats.currentEnergy < stats.dodgeEnergyCost) return;
            if (controller.dodgeCooldown == 0)
            {
                anim.SetTrigger("dodge" + s);
                controller.dodgeCooldown = controller.maxDodgeCooldown;
                health.DecreaseEnergy(stats.dodgeEnergyCost);
            }
        }

        /// <summary>
        /// Function for initial timeline cutscene
        /// </summary>
        public void DisableComponents() {
            controller.enabled = false;
            attack.enabled = false;
        }

        public void EnableComponents()
        {
            controller.enabled = true;
            controller.EnableControls();
            attack.enabled = true;
            print("wlaczam komponenty");
//            print(controller.enabled);
        }

        public void Die()
        {
            anim.SetBool("isDead", true);
            PlayerAnimations.Instance.enabled = false;
            //PlayerController.instance.enabled = false;
            GameManager.Instance.globalLight.intensity = 0;

            died = true;
        }

        public PlayerSkills GetPlayerSkills()
        {
            return playerSkills;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "ThrowableRock")
            {
                PlayerHealth.instance.DealDamage(5, Vector2.zero);
                Destroy(collision.gameObject);
            }
        }

        public void KnockDown()
        {
            anim.SetTrigger("knockDown");
            controller.controls.Disable();
        }

        public void DisableKnockDown()
        {
            controller.controls.Enable();
        }

        public void GenerateDebugLogError(string msg)
        {
            Debug.LogError(msg);
        }

        public void StartFight()
        {
            isFighting = true;
            PlayerHealth.instance.hpBar.transform.parent.gameObject.SetActive(true);

            if (!GameManager.Instance.everFought)
            {
                FightTutorial.instance.Actviate();
                GameManager.Instance.everFought = true;
            }

        }

        public void StopFight()
        {
            isFighting = false;
            PlayerHealth.instance.hpBar.transform.parent.gameObject.SetActive(false);
        }

        public void ResetPlayer()
        {
            transform.position = GameManager.Instance.currentCheckpoint.position;
            health.isDead = false;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
            health.currentHp = PlayerStats.instance.maxHp;
            stats.currentStamina = stats.maxStamina;

            Ragdoll ragdoll = GetComponent<Ragdoll>();
            ragdoll.ToggleRagdoll(false);

            anim.Rebind();
            anim.Update(0);
            anim.Play("static");

            StopFight();

            PlayerController.instance.controls.Enable();

        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.antiqueWhite;
            Gizmos.DrawWireSphere(transform.position, enemyRadius);
        }
    }

}
