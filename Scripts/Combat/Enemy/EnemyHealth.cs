using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Combat;
using Scripts.Enemy;
using Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Player;
using Scripts.Gameplay;
using Scripts.Mobs;
using System.Linq;
using UnityEngine.Events;
using Scripts.Player.Movement;

public delegate void OnDealDamage();
public delegate void OnDie();

namespace Scripts.Combat.Enemy
{
    public class EnemyHealth : MonoBehaviour, IResettable
    {
        public float currentHp;
        public float maxHp;
        public ItemDataHolder[] holders;
        private ItemData[] datas;
        public int[] amounts;
        public bool isDead;
        public GameObject txtPrefab;

        private Slider hpSlider;
        public bool isBoss;
        public BossArena bossArena;
        private Mobs.Enemy enemy;

        public event OnDealDamage dmg;

        public AudioClip[] dmgClips;
        private AudioSource source;

        public bool showSlider;

        [SerializeField] private SpriteRenderer[] spriteRenderers;
        [SerializeField] private float lootingDist = 10f;
        private Color originalColor;
        public Color hitColor;


        public event OnDie onDie;
        private PlayerController pC;

        private Coroutine hpSliderCoroutine;

        public void ResetState()
        {
            if(isDead) return;
            currentHp = maxHp;
            hpSlider.value = maxHp;
            EnemyUI.instance.HideEnemy(this);
        }

        private void Start()
        {

            datas = new ItemData[holders.Length];
            for(int i = 0; i < holders.Length; i++)
            {
                datas[i] = holders[i].data;
            }
            enemy = GetComponent<Mobs.Enemy>();

            //if (isBoss)
            //{
            hpSlider = EnemyUI.instance.CreateEnemySlider(this, enemy);
            //EnemyUI.instance.ShowEnemy(this);
            hpSlider.maxValue = maxHp;
            hpSlider.value = maxHp;
            //}
            currentHp = maxHp;

            source = GetComponent<AudioSource>();
            originalColor = spriteRenderers[0].color;
            
            pC = PlayerController.instance;
        }

        public void LootEnemy()
        {
            if(isDead)
                Looting.instance.OpenLootingUI(datas, amounts);
        }


        private void Update()
        {
           if(Vector2.Distance(transform.position, Scripts.Player.Player.main.transform.position) < lootingDist && isDead)
            {
                KeyHints.instance.ShowLootHint();
            }
            else
            {
                KeyHints.instance.HideLootHint();
            }

            // TODO: CONFIGURE WITH NEW INPUT SYSTEM - IMPORTANT!!!


            // not optimal
            // TODO: Fix for Boss (maybe IBoss or sth, not AI component) | 02.07.2026
            // showSlider = GetComponent<AI>().isAttacking && Vector2.Distance(transform.position, Scripts.Player.Player.main.transform.position) < GetComponent<AI>().raycastDistance;
            /*if (showSlider)
            {
                EnemyUI.instance.ShowEnemy(this);
            }
            else
            {
                EnemyUI.instance.HideEnemy(this);
            }*/

            /*if (Input.GetKeyDown(KeyCode.L))
            {
                Die();
            }*/
        }

        private Coroutine flashCoroutine;

        public void DealDamage(float value)
        {
            // ui 
            EnemyUI.instance.DealDamage(this, value);

            currentHp -= value;

            Debug.Log("im dealing damage from " + gameObject.name);

            //hpSlider.value = currentHp;
            float startValue = hpSlider.value;
            float targetValue = hpSlider.value - value;
            if (hpSliderCoroutine != null) StopCoroutine(hpSliderCoroutine);
            hpSliderCoroutine = StartCoroutine(AnimatedSlider(targetValue, startValue));
            Debug.Log("Damaged " + gameObject.name);
            // GetComponent<Animator>().SetTrigger("TakeHit");
            GameObject txt = Instantiate(txtPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            txt.GetComponent<DamageText>().dmg = value;
            if (currentHp <= 0)
            {
                //Debug.LogError("DELETE FUCKING APPLICATION.QUIT FROM ENEMYHEALTH.CS");
                //Application.Quit();
                print(enemy.name + " died");
                Die();
            }

            if (dmgClips.Length > 0 ) {
                AudioClip clip = dmgClips[UnityEngine.Random.Range(0, dmgClips.Length)];
                source.PlayOneShot(clip);
            }
            
            dmg.Invoke();
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            StartCoroutine(FlashRed());
        }


        private IEnumerator FlashRed()
        {
            foreach(var spriteRenderer in spriteRenderers) {
                spriteRenderer.color = hitColor;
            }
            yield return new WaitForSeconds(0.15f);
            foreach(var spriteRenderer in spriteRenderers) {
                spriteRenderer.color = originalColor;
            }
        }


        private void Die()
        {
            if (isBoss)
            {
                bossArena.BossStop();
            }
            Scripts.Player.Player.main.StopFight();
            EnemyUI.instance.HideEnemy(this);
            if(GetComponent<AI>() != null) GetComponent<AI>().enabled = false;
            EnemyUI.instance.HideEnemy(this);
            currentHp = 0;
            var ragdoll = GetComponent<Ragdoll>();
            if (ragdoll != null) ragdoll.ToggleRagdoll(true);
            var interactable = GetComponent<Interactable>();
            if (interactable != null) interactable.enabled = true;
            isDead = true;
            Scripts.Player.Player.main.StopFight();
            onDie?.Invoke();
        }
        
        IEnumerator AnimatedSlider(float targetValue, float startValue /*Image image*/)
        {
            float duration = 0.75f;
            float elapsedTime = 0;

            float startVal = startValue;
            while (elapsedTime < duration)
            {
                float val = Mathf.Lerp(startVal, targetValue, elapsedTime / duration);
                //image.fillAmount = val;
                elapsedTime += Time.deltaTime;
                hpSlider.value = val;
                yield return null;
            }
            hpSlider.value = targetValue;
            //image.fillAmount = targetValue;
        }

    }
}