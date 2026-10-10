using System;
using System.Collections;
using Scripts.CameraExtras;
using Scripts.Combat;
using Scripts.Combat.Enemy;
using Scripts.Combat.Player;
using Scripts.Player;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Enemy
{
    public enum PigState
    {
        Idle,
        Hand_Attack,
        Axe_Attack,
        Shockwave_Attack,
        Throw_Attack,
        Return_For_Axe,
        Run_Attack,
        Jump_Attack,
        Choose_Attack,
        Stunned
    }

    public class PigAI : AI
    {
        private EnemyHealth hp;
        public PigState currentState;
        [SerializeField] private UnityEngine.UI.Text stateText;

        [SerializeField] private int phase = 1;
        private int currentAttack = -1;
        private int previousAttack = -1;
        public float jumpHeight;
        public bool attacked = false;

        [Header("Jumping Attack")] public float jumpAttackDistance;
        public Vector2 jumpAttackOffset;
        public GameObject groundParticles;

        private bool performJumpingAttack;
        public bool performBase = false;
        public bool isJumping = false;
        public bool axeThrown = false;
        [SerializeField] private Transform groundCheck;
        public GameObject shockwaveSegmentPrefab;
        public int shockwaveSegmentsCount;
        private Vector2 runTarget;

        private bool isRunningAttack = false;

        private Rigidbody2D rb;

        private bool isGrounded;
        [SerializeField] private float groundRadius;

        [SerializeField] private GameObject axe;
        [SerializeField] private Transform axeParent;

        private Vector2 _axePosition;

        private EnemyHealth enemyHealth;
        private Mobs.Enemy enemy;
        public float speedModifier;
        
        private KnockBack knockBack;

        private Vector2 axeStartPos;
        private Quaternion axeStartRot;

        public BoxCollider2D axeWeaponCollider;

        protected override void Start()
        {
            base.Start();
            hp = GetComponent<EnemyHealth>();
            hp.dmg += DealDamage;
            scale = transform.localScale.x;
            rb = GetComponent<Rigidbody2D>();
            enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.dmg += OnDealDamage;
            stun.onStunActivated += OnMyStunActivated;

            enemy = GetComponent<Mobs.Enemy>();

            knockBack = GetComponent<KnockBack>();

            axeStartPos = axe.transform.localPosition;
            axeStartRot = axe.transform.localRotation;

            axeWeaponCollider = axe.GetComponent<BoxCollider2D>();
            axeWeaponCollider.enabled = false;
        }

        void OnMyStunActivated()
        {
            OnStunActivated();
            anim.SetTrigger("Stun");
            currentAttack = -1;
            ChangeState(PigState.Stunned);
        }

        private void OnDealDamage()
        {
            if (phase == 1 && enemyHealth.currentHp <= enemyHealth.maxHp / 2)
            {
                phase = 2;
            }
        }

        public void PerformJumpAttack()
        {
            // make dmg area
            Collider2D other = Physics2D.OverlapCircle((Vector2)transform.position + jumpAttackOffset,
                jumpAttackDistance,
                LayerMasks.instance.playerMask);
            if (other != null)
            {
                player.GetComponent<PlayerHealth>().DealDamage(10, -transform.right);
            }

            
            // CinemachineEffects.instance.ShakeCamera(3, 1f);
            enemy.ShakeCamera();
            Instantiate(groundParticles, transform.position, quaternion.identity);

            FinishAttack();
        }

        private void HandleState()
        {
            switch (currentState)
            {
                case PigState.Idle:
                    LookForPlayer();
                    if (isAttacking)
                    {
                        Player.Player.main.StartFight();
                        Player.Player.main.GetComponent<Attack>().currentEnemy = GetComponent<Mobs.Enemy>();
                        EnemyUI.instance.ShowEnemy(GetComponent<EnemyHealth>());
                        ChangeState(PigState.Choose_Attack);
                    }
                    else
                    {
                        if(Player.Player.main.isFighting) {

                            if (Player.Player.main.GetComponent<Attack>().currentEnemy == GetComponent<Mobs.Enemy>())
                            {
                                EnemyUI.instance.HideEnemy(GetComponent<EnemyHealth>());
                                Player.Player.main.StopFight();
                            }
                        }
                    }

                    break;

                case PigState.Choose_Attack:
                    ChooseAttack();
                    break;

                case PigState.Hand_Attack:
                    AssignAttack(OnHandAttack);
                    BaseAI();
                    break;
                
                case PigState.Shockwave_Attack:
                    AssignAttack(Shockwave);
                    BaseAI();
                    break;
                
                case PigState.Throw_Attack:
                    ThrowAttack();
                    break;
                
                case PigState.Return_For_Axe:
                    ReturnForAxe();
                    break;
                
                case PigState.Run_Attack:
                    RunLogic();
                    break;
                case PigState.Jump_Attack:
                    JumpAttack();
                    break;
                
                case PigState.Axe_Attack:
                    AssignAttack(AxeAttack);
                    BaseAI();
                    //AxeAttack();
                    //axe.gameObject.SetActive(true);
                    break;
                
                case PigState.Stunned:
                    if (stun.activated)
                    {
                        Debug.Log("Im stunned");
                        currentCooldown = maxCooldown;
                        stun.activated = false;
                        ChangeState(PigState.Choose_Attack);
                    }
                    break;
            }
        }

        private void ReturnForAxe()
        {
            // todo: make it better XD
            if (phase == 2 && axeThrown)
            {
                
                if (Vector3.Distance(transform.position, _axePosition) > 10)
                {
                    var targetPos = new Vector2(_axePosition.x, transform.position.y);
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * speedModifier * Time.deltaTime);
                    anim.SetFloat("moveSpeed", 1);
                }
                else
                {
                    print("wylaczam");
                    anim.SetFloat("moveSpeed", 0);
                    axeThrown = false;
                    axe.transform.SetParent(axeParent);
                    axe.transform.localPosition = axeStartPos;
                    axe.transform.localRotation = axeStartRot;
                    FinishAttack();
                    ChangeState(PigState.Choose_Attack);
                    axeWeaponCollider.enabled = false;
                    axe.GetComponent<EnemyWeapon>().activated = false;
                }
            }
        }

        private void ChangeState(PigState newState)
        {
            if (currentState == newState) return;
            currentState = newState;

            print($"Zmieniam stan na {newState}");
        }

        private void AssignAttack(Action action)
        {
            onAttack = action;
        }

        void Update()
        {
            // todo: remove after testing
            stateText.text = $"PigState: {currentState.ToString()}";
            // todo: -----------

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, LayerMasks.instance.groundMask);

            //LookForPlayer();
            CheckVariables();
            //Logic();

            HandleState();
            
            if (isJumping && isGrounded && rb.linearVelocity.y <= 0.1f)
            {
                rb.linearVelocity = Vector2.zero;

                currentCooldown = maxCooldown;
                attacked = false;
                isJumping = false;
                
                ChangeState(PigState.Choose_Attack);
            }

            if (currentState == PigState.Axe_Attack)
            {
                if (weapon.activated)
                {
                    weapon.activated = false;
                }
            }

        }

        void ChooseAttack()
        {
            weapon.activated = false;
            if(attacked) return;
            if (currentCooldown == 0)
            {
                if (phase == 1)
                {
                    if (previousAttack == 2)
                    {
                        currentAttack = 3;
                    }
                    else
                    {
                        currentAttack = Random.Range(0, 4);
                    }
                }
                else
                {
                    currentAttack = Random.Range(0, 3);
                }

                previousAttack = currentAttack;
                attacked = true;
            }
            else
            {
                currentAttack = -1; // nic nie rób
            }
            
            if (currentAttack == -1) return;
            if (PlayerHealth.instance.isDead) return;
            switch (currentAttack)
            {
                case 0:
                    if (phase == 1) currentState = PigState.Hand_Attack;
                    if (phase == 2) currentState = PigState.Axe_Attack;
                    break;
                case 1:
                    if (phase == 1) currentState = PigState.Shockwave_Attack;
                    if (phase == 2) currentState = PigState.Throw_Attack;
                    break;
                case 2:
                    currentState = PigState.Run_Attack;
                    break;
                case 3:
                    currentState = PigState.Jump_Attack;
                    break;
            }
        }

        private void Shockwave()
        {
            blockBaseMovement = true;
            anim?.SetFloat("moveSpeed", 0);
            anim.SetTrigger("Shockwave");
            //FinishAttack();
        }
        public void ManageSegments()
        {
            var startPos = transform.position;
            for (int i = 0; i < shockwaveSegmentsCount; i++)
            {
                var spacing = shockwaveSegmentPrefab.transform.localScale.x;
                var pos = Vector2.zero;
                if (transform.localScale.x > 0)
                {
                    pos = new Vector2(startPos.x - spacing * i, startPos.y);
                }
                else
                {
                    pos = new Vector2(startPos.x + spacing * i, startPos.y);
                }

                var segment = Instantiate(shockwaveSegmentPrefab, pos, Quaternion.identity)
                    .GetComponent<ShockwaveSegment>();
                segment.gameObject.SetActive(false);
                segment.direction = new Vector2(Mathf.Sign(transform.localScale.x), 0);
                StartCoroutine(ActivateSegments(segment, 0.5f * i));
            }
        }

        private IEnumerator ActivateSegments(ShockwaveSegment segment, float delay)
        {
            yield return new WaitForSeconds(delay);
            segment.gameObject.SetActive(true);
            yield return new WaitForSeconds(segment.activeTime);
            segment.gameObject.SetActive(false);
            Destroy(segment.gameObject);
        }


        private void RunLogic()
        {
            // zabezpieczenie
            if (runTarget.x > transform.position.x && transform.localScale.x > 0
                || runTarget.x < transform.position.x && transform.localScale.x < 0)
            {
                RotateNPC();
            }
            
            if (!isRunningAttack)
            {
                RotateNPC();
                anim.SetBool("isRunning", true);
                float dist = player.transform.position.x - transform.position.x;

                float dir = Mathf.Sign(player.transform.position.x - transform.position.x);

                // Obliczamy punkt za graczem
                float overshoot = 20f; // ile pikseli/m jednostek za gracza ma przebiec
                runTarget = new Vector2(
                    player.transform.position.x + dir * overshoot,
                    transform.position.y
                );

                isRunningAttack = true;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, runTarget, moveSpeed * 2 * Time.deltaTime);
                if (Mathf.Abs(transform.position.x - runTarget.x) < 0.5f)
                {
                    ChangeState(PigState.Choose_Attack);
                    isRunningAttack = false;
                    anim.SetBool("isRunning", false);
                    FinishAttack();
                    currentCooldown = maxCooldown;
                }
            }
        }

        public void ThrowAttack()
        {
            anim?.SetFloat("moveSpeed", 0);
            anim.SetTrigger("AxeThrow");
            attacked = true;
        }

        public void AxeThrow()
        {
            if (axeThrown) return;
            RotateNPC();
            axe.transform.parent = null;
            anim?.SetFloat("moveSpeed", 0);
            var axePos = player.transform.position;
            axeWeaponCollider.enabled = true;
            LeanTween.moveLocal(axe, axePos, 1).setOnComplete(() =>
            {
                axeThrown = true;
                ChangeState(PigState.Return_For_Axe);
            });
            _axePosition = axePos;
            attacked = true;
            print("wyrzucam siekiere");
        }

        private void AxeAttack()
        {
            anim?.SetFloat("moveSpeed", 0);
            anim.SetTrigger("AxeAttack");
            ChangeState(PigState.Choose_Attack);

        }

        private void OnAxeAttack()
        {
            anim?.SetFloat("moveSpeed", 0);
            anim.SetTrigger("AxeAttack");
           // ChangeState(PigState.Choose_Attack);
        }

        private void JumpAttack()
        {
            if (isJumping) return;
            RotateNPC();
            var dist = player.transform.position.x - transform.position.x;
            var dir = new Vector2(dist, jumpHeight);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(dir, ForceMode2D.Impulse);
            isJumping = true;
            performJumpingAttack = true;
            anim.SetTrigger("isJumping");
        }

        void OnHandAttack()
        {
            anim.SetTrigger("isFighting");
            anim?.SetFloat("moveSpeed", 0);
            ChangeState(PigState.Choose_Attack);
        }

        public void FinishAttack()
        {
            print("wywoluje finish attack");
            currentCooldown = maxCooldown;
            attacked = false;
            axeThrown = false;
            isRunningAttack = false;
            ChangeState(PigState.Choose_Attack);

            blockBaseMovement = false;
        }

        private void DealDamage()
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;

            knockBack.CallKnockBack(direction * 3.5f, Vector2.up, 0);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            if (raycastDistance == 0) return;
            Gizmos.DrawWireSphere(transform.position + offset, raycastDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + offset, minDistance);
            Gizmos.color = Color.purple;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
            Gizmos.color = Color.burlywood;
            Gizmos.DrawWireSphere((Vector2)transform.position + jumpAttackOffset, jumpAttackDistance);
        }
    }
}