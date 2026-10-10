using System;
using System.Collections;
using Ink.UnityIntegration;
using Scripts.Dialogues;
using Scripts.Inventory;
using UnityEngine;

namespace Scripts.NPC
{
    public class Woody : MonoBehaviour
    {
        private bool ritual = false;
        private bool burnt = false;

        //public RitualInventory inv;
        //private RitualInventoryUI ui;

        //[SerializeField] private ItemData[] dataOnStart, expectedItemData;
        //[SerializeField] private int[] amountsOnStart;
        [SerializeField] private Transform bubblePos;
        [SerializeField] private ParticleSystem fire;

        string[] chatbubble_texts = {"*cough*", "Little boy, come here...", "..."};

        //private bool ritualCreated = false;

        public InkFile inkFile;

        private int maxTime = 10;
        [SerializeField] private float fireTime = 40;

        private void Awake()
        {
           // ui = inv.gameObject.GetComponent<RitualInventoryUI>();
        }

        private void Start()
        {
            //ui.onRitualCompleted += RitualCompleted;
            StartCoroutine(ChatBubbleRoutine());
        }

        public void BurnTree()
        {
            print("BURN TREE!@!!");
            fire.gameObject.SetActive(true);
            burnt = true;
            StartCoroutine(Fire());
        }

        IEnumerator Fire()
        {
           yield return new WaitForSeconds(fireTime);
           fire.gameObject.SetActive(false);
           // todo: CHANGE WOODY SPRITE TO BURNT
        }

        IEnumerator ChatBubbleRoutine()
        {
            while (!burnt)
            {
                yield return new WaitForSeconds(maxTime);
                if (DialogueManager.instance.isTalking) continue;
                var text = chatbubble_texts[UnityEngine.Random.Range(0, chatbubble_texts.Length)];
                ChatBubble.Create(bubblePos, Vector3.zero, text, 8);
            }
        }

       /* void RitualCompleted()
        {
            Debug.Log("Skompletowalem rytual: Woody");
            inv.gameObject.SetActive(false);
            // uruchom kolejny dialog
            // uruchom animacje
            // TODO: Wypierdolic?
            //GetComponent<DialogueActivator>().oneTimeToUse = true;
           // DialogueManager.instance.StartDialogue(inkAsset);
        }
*/
        /*public void SetRitual()
        {
            if (!ritualCreated)
            {
                inv.SetupRitual(dataOnStart, amountsOnStart, expectedItemData);
                inv.Initialize();
                ritualCreated = true;
            }

            inv.gameObject.SetActive(true);
        }*/
    }
}