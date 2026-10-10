using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Mobs;

namespace Scripts.Combat.Enemy
{
    public class EnemyUI : MonoBehaviour
    {
        
        public static EnemyUI instance { get; private set; }
        
        // Slider prefab
        [Header("UI")]
        [SerializeField] private Slider hpSlider;

        [SerializeField] private Transform UI;
        private Dictionary<EnemyHealth, Slider> enemies = new Dictionary<EnemyHealth, Slider>();

        private Coroutine sliderCoroutine;

        
        public void DealDamage(EnemyHealth health, float amount)
        {
            if (enemies.ContainsKey(health))
            {
                Slider s = enemies.GetValueOrDefault(health);
                float difference = health.currentHp - amount;
                if (sliderCoroutine != null)
                {
                    StopCoroutine(sliderCoroutine);
                }
                sliderCoroutine = StartCoroutine(AnimatedSlider(difference, health.currentHp, s));
            }
        }
        
        IEnumerator AnimatedSlider(float targetValue, float startValue, Slider s)
        {
            float duration = 0.75f;
            float elapsedTime = 0;

            float startVal = startValue;
            while (elapsedTime < duration)
            {
                float val = Mathf.Lerp(startVal, targetValue, elapsedTime / duration);
                s.value = val;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            s.value = targetValue;
        }

        public Slider CreateEnemySlider(EnemyHealth health, Scripts.Mobs.Enemy enemy)
        {
            Slider s = Instantiate(hpSlider, UI);
            s.name = "[HP] " + enemy.enemyName;
            enemies.Add(health, s);
            s.gameObject.SetActive(false);
            s.GetComponentInChildren<Text>().text = enemy.enemyName;

            s.maxValue = health.maxHp;
            s.value = health.currentHp;
            
            return s;
        }
        
        public Slider ShowEnemy(EnemyHealth health)
        {
            Slider s;
            if (enemies.ContainsKey(health))
            {
                s = enemies.GetValueOrDefault(health);
                s.gameObject.SetActive(true);
            }
            else
            {
                s = CreateEnemySlider(health, health.GetComponent<Mobs.Enemy>());
                s.gameObject.SetActive(true);
                //s.gameObject.SetActive(true);
              //  enemies.Add(health, s);
            }

            return s;
        }

        public void HideEnemy(EnemyHealth health)
        {
            if (enemies.ContainsKey(health) && enemies.GetValueOrDefault(health).IsActive())
            {
                Debug.Log("Enemy is already added!");
                Slider s = enemies.GetValueOrDefault(health);
                s.gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
            Debug.LogWarning($"EnemyUI is located in {transform.gameObject.name} inside of {transform.root.gameObject.name}.");
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogError("More than 1 EnemyUI instance!");
                Destroy(this);
            }
        }
    }
}