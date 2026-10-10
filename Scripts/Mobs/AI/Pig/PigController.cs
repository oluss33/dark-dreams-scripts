using System;
using Scripts.Combat.Enemy;
using UnityEngine;

namespace Scripts.Enemy
{
    public class PigController : MonoBehaviour
    {
        public bool pressedOnce = false;

        public PigAI pigAI;
        public EnemyHealth enemyHealth;
        
        private void OnMouseDown()
        {
            if (pressedOnce)
            {
                Debug.Log("Rozkurwiony świnio");
                pigAI.enabled = true;
                enemyHealth.enabled = true;

                Destroy(this);
            }
            if (!pressedOnce)
            {
                ChatBubble.Create(transform, new Vector2(3, 10), "#FREE ALTEN", 4);
                pressedOnce = true;
            }
        }
    }
}