using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.CameraExtras;
using UnityEngine;
using Scripts.Player;
using Scripts.Dialogues;
using Scripts.Player.Movement;
using UnityEngine.Events;
using Scripts.Sounds;
using Ink.UnityIntegration;

namespace Scripts.Dialogues
{
    public class DialogueActivator : MonoBehaviour
    {
        public InkFile inkFile;
        public string npcName;
        [HideInInspector] public NPC.NPC npc;
        private Player.Player player;
        public bool dialogueCreated = false;
        public bool oneTimeToUse = true;
        [Header("Distance")] public float activeDist;

    //    private Dictionary<Message, UnityEvent> events = new Dictionary<Message, UnityEvent>();

        void Start()
        {
            try
            {
                npc = NPC.NPCManager.instance[npcName];
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogWarning("NPC named '" + npcName + "' not found. " + e);
            }
            catch (ArgumentNullException e)
            {
                Debug.LogWarning("@DialogueActivator: NullReferenceException " + e);
            }

         /*   for (int i = 0; i < dialogue.englishMessage.optionArray.Count; i++)
            {
                dialogue.englishMessage.optionArray[i].used = false;
            }
*/
            player = Player.Player.main;
        }

        private void Update()
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= activeDist)
            {
                if (PlayerController.instance.controls.Player.Talk.WasPressedThisFrame())
                {
                    StartDialogue();
                }
            }
        }

        public void StartDialogue()
        {
            if (dialogueCreated && oneTimeToUse) return;
            if (DialogueManager.instance.isTalking) return;
            // TODO: Change this for controllers
            
                CinemachineEffects.instance.Zoom(15, 0.5f);
                Player.Player.main.source.PlayOneShot(SoundsManager.swoosh);
                DialogueManager.instance.StartDialogue(inkFile, npc);
                dialogueCreated = true;
        }

        private void OnDrawGizmosSelected()
        {

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, activeDist);
        }
    }
}
