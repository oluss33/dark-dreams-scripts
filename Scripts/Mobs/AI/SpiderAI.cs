using System;
using Scripts.Combat.Enemy;
using Scripts.Combat.Player;
using Scripts.Player;
using Scripts.Player.Movement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Enemy
{
    public class SpiderAI : AI
    {
        [SerializeField] private AudioClip[] sounds;
        private int randomAttack = 0;

        private EnemyHealth hp;
        private KnockBack knockBack;
        private bool isPerformingAttack = false;

        private bool test_1 = false;

        public enum SpiderState
        {
            Idle,
            Choose_Attack,
            Attack,
            Stunned
        }

        protected override void Start()
        {
            base.Start();
            hp = GetComponent<EnemyHealth>();
            hp.dmg += DealDamage;

            stun.onStunActivated += OnSpiderStunActivated;

            knockBack = GetComponent<KnockBack>();

        }

        private void OnSpiderStunActivated()
        {
            ChangeState(SpiderState.Stunned);
            anim.SetBool("stun", true);
            stun.activated = true;
            currentCooldown = maxCooldown;
            OnStunActivated();
        }

        private void DealDamage()
        {
            Vector2 direction = (transform.position - player.transform.position).normalized * 1.5f;

            knockBack.CallKnockBack(direction, Vector2.up, 0);
        }

        protected override void Update()
        {

            if (currentCooldown < 0) 
            { 
                currentCooldown = 0; 
            }

            if (currentCooldown == 0)
            {
                if (stun.activated)
                {
                    print("stun off ?");
                    // zabezpieczenie
                    anim.SetBool("stun", false);
                    stun.activated = false;
                    ChangeState(SpiderState.Idle);
                }
            }

            CheckVariables();

            HandleState();
        }


        public SpiderState currentState;

        private void HandleState()
        {
            switch (currentState)
            {
                case SpiderState.Idle:
                    LookForPlayer();
                    if (isAttacking && !stun.activated)
                    {
                        Player.Player.main.StartFight();
                        //GetComponent<EnemyHealth>().showSlider = true;
                        EnemyUI.instance.ShowEnemy(GetComponent<EnemyHealth>());
                        player.GetComponent<Attack>().currentEnemy = GetComponent<Mobs.Enemy>();

                        if(!test_1)
                        {
                            LocationsUI.instance.mooshland.AddTextToFirstDesc("I've spotted a spider. It looks really scary.");
                            test_1 = true;
                        }

                        if(currentCooldown == 0)
                            ChangeState(SpiderState.Choose_Attack);
                    }
                    else
                    {
                        if(Player.Player.main.isFighting) {
                            //GetComponent<EnemyHealth>().showSlider = false;
                            EnemyUI.instance.HideEnemy(GetComponent<EnemyHealth>());
                            if(Player.Player.main.GetComponent<Attack>().currentEnemy == GetComponent<Mobs.Enemy>())
                                Player.Player.main.StopFight();
                        }
                    }

                    break;

                case SpiderState.Choose_Attack:
                    ChooseAttack();
                    break;

                case SpiderState.Attack:
                    if(stun.activated) return;
                    RotateNPC();
                    AssignAttack(Attack);
                    BaseAI();

                    break;
                case SpiderState.Stunned:
                    print("spider stun kurwa :)");
                    break;
            }
        }

        private void ChooseAttack()
        {
            if (isPerformingAttack) return;
            weapon.activated = false;
            if (currentCooldown == 0)
            {
                randomAttack = Random.Range(0, 2);

                ChangeState(SpiderState.Attack);
            }
            else
            {
                randomAttack = -1; // nic nie rób
                ChangeState(SpiderState.Idle);
            }
        }



        private void Attack()
        {
            if(currentCooldown > 0) return;
            if(isPerformingAttack) return;
            if (PlayerHealth.instance.isDead) return;
            anim.SetTrigger("attack_" + randomAttack);
            isPerformingAttack = true;
            ChangeState(SpiderState.Idle);
        }

        public void Finish_Test_Kurwa()
        {
            ChangeState(SpiderState.Idle);
            //print("FINISH ATTACK KURWA");
            onAttack = null;
            isPerformingAttack = false;
            currentCooldown = maxCooldown;
        }

        public void PlayRandomSound()
        {
            Debug.Log(source);
            AudioClip clip = sounds[Random.Range(0, sounds.Length)];
            Debug.Log(clip);
            source.PlayOneShot(clip);
        }

        private void ChangeState(SpiderState newState)
        {
            if (currentState == newState) return;
            currentState = newState;
        }

        private void AssignAttack(Action action)
        {
            onAttack = action;
        }
    }
}