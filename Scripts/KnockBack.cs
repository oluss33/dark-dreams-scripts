using System;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class KnockBack : MonoBehaviour
    {
        public float knockbackTime = 0.2f;
        public float hitDirectionForce = 10f;
        public float constForce = 5f;
        public float inputForce = 7.5f;

        private Rigidbody2D rb;

        private Coroutine knockbackCoroutine;
        
        public bool IsBeingKnockBacked { get; private set; }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public IEnumerator KnockBackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
        {
            IsBeingKnockBacked = true;

            Vector2 _hitForce;
            Vector2 _constantForce;
            Vector2 _knockbackForce;
            Vector2 _combinedForce;

            _hitForce = hitDirection * hitDirectionForce;
            _constantForce = constantForceDirection * constForce;
            
            float _elapsedTime = 0f;
            while (_elapsedTime < knockbackTime)
            {
                _elapsedTime += Time.fixedDeltaTime;

                _knockbackForce = _hitForce + _constantForce;

                if (inputDirection != 0)
                {
                    _combinedForce = _knockbackForce + new Vector2(inputDirection * inputForce, 0);
                }
                else
                {
                    _combinedForce = _knockbackForce;
                }

                rb.linearVelocity = _combinedForce;
                
                yield return new WaitForFixedUpdate();
            }
            knockbackCoroutine = null;
            IsBeingKnockBacked = false;
        }

        public void CallKnockBack(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
        {
            if(knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);
            knockbackCoroutine = StartCoroutine(KnockBackAction(hitDirection, constantForceDirection, inputDirection));
        }
    }
}