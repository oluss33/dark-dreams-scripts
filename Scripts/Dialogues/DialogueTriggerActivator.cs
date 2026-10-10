using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player;
using Scripts.Dialogues;
using UnityEngine.Events;
using Ink.UnityIntegration;

namespace Scripts.Dialogues
{
    public class DialogueTriggerActivator : MonoBehaviour
    {
        public InkFile inkFile;
        public string npcName;
        NPC.NPC npc;
        [SerializeField] Player.Player player;
        public GameObject go;
        public bool used = false;


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

      /*      for (int i = 0; i < dialogue.englishMessage.optionArray.Count; i++)
            {
                dialogue.englishMessage.optionArray[i].used = false;
            }*/
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!used)
            {
                if (other.tag == "Player")
                {
                    Debug.Log("Dialogue Trigger Activator - Trigger Enter");
                    DialogueManager.instance.StartDialogue(inkFile, npc);
                    npc.dialogueCreated = true;
                    used = true;
                }
            }
        }
    }
}