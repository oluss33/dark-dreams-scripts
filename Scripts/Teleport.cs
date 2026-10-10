using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Scripts.Dialogues;
using Scripts.Player.Movement;

namespace Scripts {
    public class Teleport : IInteractable
    {
        [SerializeField] private AudioSource sound;
        [SerializeField] private Transform pos;
        
        private Player.Player player;

        public bool canInteract = true;

        public bool onClick = true;
        public bool onTrigger = false;
        
        public float dist;
        
        void Start() {
            player = Player.Player.main;

            //PlayerController.instance.controls.Player.Interact.performed += ctx => Interact();
        }

        public override void Interact()
        {
            if (DialogueManager.instance != null)
                if (DialogueManager.instance.isTalking)
                    return;
            if(player.isTravelling) return;
            if (!canInteract) return;
            if (!onClick) return;
            if (Vector2.Distance(player.transform.position, transform.position) > dist) return;
            Openable[] openables = FindObjectsOfType<MonoBehaviour>().OfType<Openable>().ToArray();
            print("wjebalem sie w petle");
            foreach (Openable openable in openables)
            {
                if (openable.IsOpened())
                {
                    print("wypieradlam stad");
                    return;
                }
            }
            TeleportPlayer();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (onTrigger)
            {
                if (other.CompareTag(Tags.Player))
                {
                    TeleportPlayer();
                }
            }
        }


        public void TeleportPlayer()
        {
            if(!player.isTravelling) player.isTravelling = true;
            StartCoroutine(BlackScreen.instance.TurnBlackScreen(1.5f));
            StartCoroutine(DisableTravelling(2f));
            if(sound != null) sound.Play();
            player.transform.position = new Vector3(pos.position.x, pos.position.y, 0);
            Debug.Log($"Teleport wywołany przez: {gameObject.name}");
            Debug.Log(Environment.StackTrace);
        }

        IEnumerator DisableTravelling(float sec)
        {
            yield return new WaitForSeconds(sec);
            player.isTravelling = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, dist);
        }
    }
}