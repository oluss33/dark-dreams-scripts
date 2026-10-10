using System;
using System.Collections;
using Cinemachine;
using Scripts.CameraExtras;
using UnityEngine;

namespace Scripts.Mobs
{
    public class Enemy : MonoBehaviour
    {
        public string enemyName;
        public int description { get; private set; }
      //  public float moveSpeed;
        public bool isDead;

        public GameObject blindParticles;

        
        public bool IsBlinded { get; private set; }
        
        [SerializeField] private ScreenShakeProfile profile;
        private CinemachineImpulseSource impulseSource;

        private void Start()
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public void DoBlind()
        {
            StartCoroutine(Blindness());
        }

        public void ShakeCamera()
        {
            CinemachineEffects.instance.ScreenShakeFromProfile(profile, impulseSource);
        }

        private IEnumerator Blindness()
        {
            IsBlinded = true;
            blindParticles?.gameObject.SetActive(true);
            yield return new WaitForSeconds(5);
            blindParticles?.gameObject.SetActive(false);
            IsBlinded = false;
        }
    }
}