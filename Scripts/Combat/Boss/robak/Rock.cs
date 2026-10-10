using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Combat.Worm
{
    public class Rock : MonoBehaviour
    {
        public float dmg;
        public float lifeTime;

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            
            if(lifeTime <= 0) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.tag);
            if (other.CompareTag(Tags.Player))
            {
                Debug.Log("@Worm: Player has been hit by rock");
                
                Scripts.Player.PlayerHealth.instance.DealDamage(dmg, Vector2.zero);
                
                Destroy(gameObject);
            }
            if(other.CompareTag(Tags.Ground))
            {
                Destroy(gameObject);
            }
        }
    }
}