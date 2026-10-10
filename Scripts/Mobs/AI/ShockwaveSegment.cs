using System;
using System.Collections;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Enemy
{
    public class ShockwaveSegment : MonoBehaviour
    {

        public Vector2 direction;
        public int damage;
        public float activeTime;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            print(other.tag + " lol " + other.name);
            if (other.CompareTag(Tags.Player)) 
            {
                var hp = Player.Player.main.GetComponent<PlayerHealth>();
                if (hp != null)
                {
                    hp.DealDamage(damage, direction); 
                }
            }
        }

        public IEnumerator Activate()
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(activeTime);
            gameObject.SetActive(false);
        }
    }
}