using System;
using Scripts.Combat.Enemy;
using UnityEngine;

namespace Scripts.Abilities.Functionality
{
    public class LightExplosion : MonoBehaviour
    {

        public float dmg;
        
        private void OnParticleCollision(GameObject other)
        {

            if (other.CompareTag(Tags.Enemy))
            {
                Debug.Log("I've hit enemy :)");
                var hp = other.GetComponent<EnemyHealth>();
                hp.DealDamage(dmg);
            }
        } 
    }
}