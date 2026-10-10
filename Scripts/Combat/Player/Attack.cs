using System.Collections;
using System.ComponentModel;
using Scripts.CameraExtras;
using Scripts.Combat;
using Scripts.Inventory;
using Scripts.Player;
using Scripts.Player.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.Combat.Player
{
    public class Attack : MonoBehaviour
    {
        
        //[Header("Energy")]
        //public Image energySlider;
        //public bool isUsingEnergy;
        //public float energyRegenerationSpeed;
        // TODO: ENERYG SLIDER
        public bool alreadyAttacked;
        public bool canBlock;
        
        [SerializeField] private PlayerWeapon pWeapon;
        public GameObject aboveIndicator;
        public GameObject bottomIndicator;
        public GameObject leftIndicator;
        public GameObject rightIndicator;

        //[SerializeField] private float maxEnergy;
        //[SerializeField] private float energy;
        private Item weapon;
        private Item bowWeapon;
        private PlayerControls controls;
        PlayerHealth health;
        private Animator anim;
        public bool isBlocking;
        PlayerStats stats;
        
        private AudioClip[] playerImpacts;
        private AudioClip strongAttackClip;

        public Stun stunnedEnemy;

        private CinemachineEffects camEff;

        public bool strongAttackPerformed = false;

        public float strongAttackMaxCooldownTime;
        public float strongAttackCooldownTime;

        public bool isFighting;

        public Mobs.Enemy currentEnemy;

        private Coroutine indicatorCoroutine;
        private void Start()
        {
            controls = PlayerController.instance.controls;
            controls.Attack.Attack.performed += ctx => StartAttack();
            controls.Attack.Block.performed += ctx => StartBlock();
            controls.Attack.Block.canceled += ctx => StopBlock();
            controls.Attack.Parry.performed += ctx => Parry();
            anim = GetComponent<Animator>();
            health = PlayerHealth.instance;
            stats = PlayerStats.instance; 
            //energy = maxEnergy;
            canBlock = true;

            playerImpacts = Resources.LoadAll<AudioClip>("Sounds/Player/FINAL/attack_clips");
            strongAttackClip = Resources.Load<AudioClip>("Sounds/Player/Combat/strong_attack");
            
            camEff = CinemachineEffects.instance;

            aboveIndicator.SetActive(false);
            bottomIndicator.SetActive(false);
            leftIndicator.SetActive(false);
            rightIndicator.SetActive(false);
        }

        public Item GetBow()
        {
            return bowWeapon;
        }

        public void SetBow(Item bow)
        {
            this.bowWeapon = bow;
        }

        public void ResetBow()
        {
            this.bowWeapon = new Item(new ItemData(), 0);
        }

        public void SetWeapon(Item weapon)
        {
            this.weapon = weapon;
            
//            Debug.Log("ustawiam weapon");
 //           Debug.Log(weapon.data.name);
        }

        public Item GetWeapon()
        {
            return weapon;
        }

        public void ResetWeapon()
        {
            this.weapon = new Item(new ItemData(), 0); 
        }


        public void StopAttack()
        {
            //anim.SetBool("Attack", false);
            canBlock = true;
        }
        
        public void ResetAttackStatus()
        {
            alreadyAttacked = false;
        }


        private bool canDoStrongAttack = false;
        
        private void Update()
        {
            if (stunnedEnemy != null)
            {
                if (stunnedEnemy.activated)
                {
                    canDoStrongAttack = true;
                }
                else
                {
                    canDoStrongAttack = false;
                    //stunnedEnemy = null;
                }
            }

            if (strongAttackCooldownTime > 0) strongAttackCooldownTime -= Time.deltaTime;
            if (strongAttackCooldownTime < 0) strongAttackCooldownTime = 0;
            

            if (isBlocking)
            {
                //stats.currentEnergy -= 20 * Time.deltaTime;
                PlayerHealth.instance.DecreaseEnergyDeltaTime(20);
            }

            if (stats.currentEnergy <= 0)
            {
                stats.currentEnergy = 0;
                StopBlock();
                // GetComponent<Stun>().StunMyself(); TODO: FINISH IT
            }

            /*if(!isBlocking && stats.currentEnergy < stats.maxEnergy)
            {
                stats.currentEnergy += stats.energyRegenerationSpeed * Time.deltaTime;
            }*/

            //if(energy < 100&& !isUsingEnergy)
            //{
            //    if (!isBlocking)
            //    {
            //        energy += energyRegenerationSpeed * Time.deltaTime;
            //    }
            //    else
            //    {
            //        energy += energyRegenerationSpeed/4 * Time.deltaTime;
            //    }
            ////}
            //if(energy < 0)
            //{
            //    energy = 0;
            //    StopBlock();
            //    GetComponent<Stun>().StunMyself();

            ////}
            //energySlider.fillAmount = energy / 100;
        }

        public void PlayHitClip()
        {
            pWeapon.PlayHitClip();
        }

        private void StartAttack()
        {
            if(stats.currentEnergy <= 0) return;
            if (stats.cooldown == 0)
            {
                if (anim.GetBool("Attack")) return;

                canBlock = false;
                stats.cooldown = weapon.data.cooldown;

                if (canDoStrongAttack && strongAttackCooldownTime == 0)
                {
                    PerformStrongAttack();
                    return;
                }

                int index = Random.Range(0, playerImpacts.Length);
                var playerImpact = playerImpacts[index];
                Scripts.Player.Player.main.source.PlayOneShot(playerImpact);
                
                anim.SetBool("Attack", true);
                //health.DecreaseStamina(20);
                health.DecreaseEnergy(10);

            }

        }

        void DisableStrongAttack()
        {
            anim.SetBool("StrongAttack", false);
            strongAttackPerformed = false;
        }

        void CameraZoom()
        {
            camEff.Zoom(25, .3f, () => camEff.Zoom(camEff.startSize, 1));
        }

        private void PerformStrongAttack()
        {
            strongAttackCooldownTime = strongAttackMaxCooldownTime;
            print("Strong Attack :)");
            strongAttackPerformed = true;
            anim.SetBool("StrongAttack", true);
            Scripts.Player.Player.main.source.PlayOneShot(strongAttackClip);
            

        }
        
        private void StartBlock()
        {
            if(stats.currentEnergy > 0) {
                anim.SetBool("isBlocking", true);
                isBlocking = true;
                canBlock = false;
            }
        }

        private void StopBlock()
        {
            anim.SetBool("isBlocking", false);
            isBlocking = false;
            canBlock = true;
        }
        private void Parry()
        {
            if(stats.currentEnergy < stats.parryEnergyCost) return;
            if (stats.cooldown == 0)
            {
                Debug.Log("im parring");
                anim.SetTrigger("Parry");
                health.DecreaseEnergy(stats.parryEnergyCost);
                stats.cooldown = weapon.data.cooldown;
            }
        }

        private void HideAllIndicators()
        {
            leftIndicator.SetActive(false);
            rightIndicator.SetActive(false);
            aboveIndicator.SetActive(false);
            bottomIndicator.SetActive(false);
        }

        public void ShowIndicator(GameObject indicator, float time)
        {
            if (indicatorCoroutine != null) { return; }

            HideAllIndicators();

            indicatorCoroutine = StartCoroutine(ShowIndicatorCoroutine(indicator, time));
        }

        IEnumerator ShowIndicatorCoroutine(GameObject indicator, float time)
        {
            indicator.SetActive(true);
            yield return new WaitForSeconds(time);
            indicator.SetActive(false);
            indicatorCoroutine = null;
        }
    }
}