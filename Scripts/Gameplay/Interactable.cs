using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Player.Movement;
using UnityEngine;
using UnityEngine.UI;


namespace Scripts.Gameplay {
    public class Interactable : MonoBehaviour
    {
        [HideInInspector] public SpriteRenderer s;
        public Color normalColor;
        public Color highlightedColor;

        public string highlightedString;

        public bool IsNPC;
       private bool showTooltip;

       public bool used = false;

       public Action onPlayerOverlap;

        void Awake() {
            s = GetComponent<SpriteRenderer>();
        }

       /* private void Update()
        {
            if (showTooltip)
            {
                if (interactionText == null) return;
                interactionText.gameObject.transform.position = Input.mousePosition;
            }
        }*/

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(used) return;
            if (Dialogues.DialogueManager.instance != null) 
                if (Dialogues.DialogueManager.instance.isTalking) 
                    return;
            if (other.CompareTag(Tags.Player))
            {

                //interactionText.text = highlightedString;
                //interactionText.gameObject.SetActive(true);
                s.color = highlightedColor;
                if (!IsNPC) {
                    KeyHints.instance.ShowInteractHint();
                    onPlayerOverlap?.Invoke();
                }
                else
                    KeyHints.instance.ShowTalkHint();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag(Tags.Player))
            {
                s.color = normalColor;
                KeyHints.instance.HideInteractHint();
                KeyHints.instance.HideTalkHint();
                //if (interactionText != null)
                //    interactionText.gameObject.SetActive(false);
            }
        }

        /*void OnMouseEnter() {
            if (Dialogues.DialogueManager.instance != null) 
                if (Dialogues.DialogueManager.instance.isTalking) 
                    return;
            s.color = highlightedColor;
            if (interactionText != null)
            {
                interactionText.text = highlightedString;
                interactionText.gameObject.SetActive(true);
            }

            showTooltip = true;
        }

        void OnMouseExit() {
            s.color = normalColor;
            showTooltip = false;
            if(interactionText != null)
            interactionText.gameObject.SetActive(false);
        }*/
    }
}
