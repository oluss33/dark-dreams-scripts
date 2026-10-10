using Scripts.Combat.Player;
using Scripts.Player.Movement;
using System;
using System.Collections;
using Scripts.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Player
{
    [DefaultExecutionOrder(-1000)]
    public class PlayerHealth : MonoBehaviour
    {
        public Image hpBar;
        public Image staminaBar;
        public Image energyBar;
       // public Image strengthBar; // propably deprecated
            // public Image delayBar;

        public float currentHp;

        public DamageText txtPrefab;
        
        //public float currentStamina;
        //public float maxStamina;

        public float staminaRegenerationSpeed;

        public int protection;

        public static PlayerHealth instance;
        private PlayerStats stats;
        private PlayerController controller;
        private Attack attack;
        public bool isUsingStamina;
        public bool isUsingEnergy;

        public AudioClip[] dmgClips;

        private AudioClip dmgClip;

        private KnockBack knockBack;

        public bool isDead = false;

        private Coroutine energyCoroutine;
        
        public void SetMaxHp(float v)
        {
            stats.maxHp = v;
            hpBar.fillAmount = currentHp / stats.maxHp;
        }
        
        private void Awake()
        {
            instance = this;
        }
        
        private void Start()
        {
            controller = GetComponent<PlayerController>();
            attack = GetComponent<Attack>();
            stats = PlayerStats.instance;
            currentHp = stats.maxHp;

            staminaRegenerationSpeed = stats.staminaRegenerationSpeed;
            stats.maxStamina = 100;
            stats.currentStamina = stats.maxStamina;
            
            hpBar.fillAmount = stats.maxHp / 100;
            staminaBar.fillAmount = stats.currentStamina / stats.maxStamina;
            energyBar.fillAmount = stats.currentEnergy / stats.maxEnergy;
            
            hpBar.transform.parent.gameObject.SetActive(true);
            staminaBar.transform.parent.gameObject.SetActive(true);
            energyBar.transform.parent.gameObject.SetActive(true);
            //strengthBar.gameObject.SetActive(false);

            knockBack = GetComponent<KnockBack>();
        }

        private void Update()
        {

            if (stats.currentStamina < stats.maxStamina)
            {
                AddStamina();
            }

            if (stats.energyRegenDelay > 0)
            {
                stats.energyRegenDelay -= Time.deltaTime;
            }
            else
            {
                stats.energyRegenDelay = 0;
            }

            if(stats.currentEnergy < stats.maxEnergy && stats.energyRegenDelay <= 0)
            {
                AddEnergy();
            }
            
            // debug todo remove
            if (Input.GetKeyDown(KeyCode.K))
            {
                Death();
            }
        }

        //private void Update()
        //{
        //    if (isExhausted)
        //    {
        //        if(stats.staminaRegenerationDelayCounter <= 0)
        //        {
        //            GetComponent<Animator>().SetTrigger("forceStatic");
        //        }
        //    }
        //}

        IEnumerator AnimatedSlider(float targetValue, float startValue, Image image)
        {
            float duration = 0.75f;
            float elapsedTime = 0;

            float startVal = startValue;
            while (elapsedTime < duration)
            {
                float val = Mathf.Lerp(startVal, targetValue, elapsedTime / duration);
                image.fillAmount = val;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.fillAmount = targetValue;
        }

        public void AddEnergy()
        {
            if(isUsingEnergy) return;

            stats.currentEnergy += stats.energyRegenerationSpeed * Time.deltaTime;

            if (stats.currentEnergy > stats.maxEnergy)
            {
                stats.currentEnergy = stats.maxEnergy;
            }
            if (stats.currentEnergy < 0f)
            {
                stats.currentEnergy = 0f;
            }
            energyBar.fillAmount = stats.currentEnergy / stats.maxEnergy;
        }

        public void AddStamina() //run on update
        {
            if (isUsingStamina) return;

            stats.currentStamina += stats.staminaRegenerationSpeed * Time.deltaTime;

            if (stats.cooldown > 0)
            {
                stats.cooldown -= Time.deltaTime;
            }

            if (stats.cooldown < 0)
            {
                stats.cooldown = 0;
            }
            if (stats.currentStamina > stats.maxStamina)
            {
                stats.currentStamina = stats.maxStamina;
            }

            if (stats.currentStamina < 0f)
            {
                //controller.isRunning = false;
               // stats.staminaRegenerationDelayCounter += Math.Abs(stats.currentStamina);
                stats.currentStamina = 0f;
            }

            staminaBar.fillAmount = stats.currentStamina / stats.maxStamina;
        }

        public void DecreaseEnergy(float amount)
        {
            if(energyCoroutine != null)
            {
                StopCoroutine(energyCoroutine);
            }
            energyCoroutine = StartCoroutine(AnimatedSlider((stats.currentEnergy - amount) / stats.maxEnergy, energyBar.fillAmount, energyBar));
            stats.currentEnergy -= amount;

            stats.energyRegenDelay = stats.energyRegenDelayTime;


        }

        public void DecreaseEnergyDeltaTime(float amount)
        {
            stats.currentEnergy -= amount * Time.deltaTime;
            if (stats.currentEnergy < 0f)
            {
                stats.currentEnergy = 0f;
            }
            energyBar.fillAmount = stats.currentEnergy / stats.maxEnergy;
            energyBar.transform.parent.gameObject.SetActive(true);
            stats.energyRegenDelay = stats.energyRegenDelayTime;
        }

        public void DecreaseStamina(float amount)
        {
            stats.currentStamina -= amount * Time.deltaTime;
            staminaBar.fillAmount = stats.currentStamina / stats.maxStamina;

            if (stats.currentStamina > stats.maxStamina)
            {
                stats.currentStamina = stats.maxStamina;
            }

        }

        private void GenerateDmgClip()
        {
            int index = UnityEngine.Random.Range(0, dmgClips.Length);
            dmgClip = dmgClips[index];
        }
        public void DealDamage(float amount, Vector2 hitDirection)
        {
            GenerateDmgClip();
            if (stats.isPlayerResilient)
            {
                Debug.Log("I'm resilient!");
                // TODO: Make some visual effects :)
                return;
            }
            
            float tempValue = currentHp -= (amount - protection);
            currentHp = tempValue;
            if (currentHp < 0) currentHp = 0;
            float targetValue = currentHp / 100;
            
            StartCoroutine(AnimatedSlider(targetValue, hpBar.fillAmount, hpBar));

            DamageText txt = Instantiate(txtPrefab, Player.main.transform.position, Quaternion.identity);
            txt.dmg = amount;
            
            Player.main.source.PlayOneShot(dmgClip);

            
            if (currentHp <= 0)
            {
                currentHp = 0;
                if(!isDead)
                    Death();
                
                // SceneManager.LoadScene("Demo");
            }
            else
            {
                knockBack.CallKnockBack(hitDirection, Vector2.up, PlayerController.instance.GetDirection().x);
            }
        }

        public void Death()
        {
            
            isDead = true;
            print("smierc :(");
            // toggle ragdoll
            GetComponent<Ragdoll>().ToggleRagdoll(true);
            
            // play sound
            // TODO: Move it to PlayerSounds.cs
            var clips = PlayerSounds.instance.dyingSounds;
            
            int index = UnityEngine.Random.Range(0, clips.Length);

            var clip = clips[index];

            Player.main.source.PlayOneShot(clip);
            
            // show death ui
            StartCoroutine(ShowDeathScreen(3.5f));
            
            // disable controls
            PlayerController.instance.controls.Disable();
            
            // Todo: Respawn
            // Todo: Loading variables after respawn
        }

        private IEnumerator ShowDeathScreen(float t)
        {
            yield return new WaitForSeconds(t);
            DeathUI.instance.ShowUI();
        }
        
        public void AddHp(int amount)
        {
            float tempValue = currentHp += amount;
            currentHp = tempValue;
            if (currentHp > stats.maxHp) currentHp = stats.maxHp;
            float targetValue = currentHp / 100;
            StartCoroutine(AnimatedSlider(targetValue, hpBar.fillAmount, hpBar));

            VignetteManager.instance.SetVignette();

            if (currentHp <= 0)
            {
                currentHp = 0;
                Debug.Log("Player is dead");
            }
        }

       /* public void CheckStamina()
        {
            if(stats.staminaRegenerationDelayCounter > 0)
            {
                GetComponent<Animator>().SetTrigger("interrupt");
            }
        }*/

        public void KnockDown()
        {
            GetComponent<Animator>().SetTrigger("knockDown");
        }
    }
}