using System;
using System.Collections;
using Scripts.Gameplay;
using Scripts.Player;
using UnityEngine;

namespace Scripts {
    public class BossActivator : IInteractable
    {
        public BossArena arena;
        [SerializeField] private float timeToStart;
        private bool activated = false;
        [SerializeField] private bool showAfterReset;
        public override void Interact()
        {
            if(activated) return;
            arena.ActivateBarrier();
            StartCoroutine(Temp_Start());
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInChildren<GameObject>()?.SetActive(false);
        }

        public void Reset()
        {
            if(!showAfterReset) return;
            activated = false;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponentInChildren<GameObject>()?.SetActive(true);
            GetComponent<Interactable>().enabled = true; 
            GetComponent<Interactable>().used = false; 
        }

        IEnumerator Temp_Start()
        {
            yield return new WaitForSeconds(timeToStart);
            arena.BossStart();
        }
    }
}