
using System;
using Ink.Runtime;
using Ink.UnityIntegration;
using Scripts.Inventory;
using Scripts.NPC;
using UnityEngine;

namespace Scripts.Dialogues
{
    public class DialogueExternalFunctions : MonoBehaviour
    {
        [Header("Amanda")]
        public SpriteRenderer amanda;
        public Sprite amandaWithoutCardboard;
        public Sprite sadAmanda;

        [Header("Ghost")]
        public SpriteRenderer ghost;

        public Sprite angryGhost;
        public Sprite sadGhost;
        public Sprite happyGhost;

        [Header("Woody")] 
        public SpriteRenderer woody;

        public InkFile drzewo2;

        public static DialogueExternalFunctions instance { get; private set; }

        private void Awake()
        {
            if (instance == null) {
                instance = this;
            } 
            else
            {
                Destroy(gameObject);
                print("more than 1 external functions instance");
            }
        }

        public void Bind(Story story)
        {

            story.BindExternalFunction("Ritual", () =>
            {
                print("TODO RITUAL CUTSCENE??");
            });
            
            story.BindExternalFunction("BurnTree", () =>
            {
                FindAnyObjectByType<Woody>().BurnTree();
            });
            
            
            story.BindExternalFunction("ChangeDialogue", (string dialogueName) =>
            {
                switch (dialogueName)
                {
                    case "Drzewo2":
                        woody.GetComponent<DialogueActivator>().dialogueCreated = false;
                        woody.GetComponent<DialogueActivator>().inkFile = drzewo2;
                        woody.GetComponent<DialogueActivator>().oneTimeToUse = false;
                        break;
                }
            });

            story.BindExternalFunction("GaveSweet", (int amount) =>
            {
                FindObjectOfType<Boy>().gaveSweet = true;
            });
            
            story.BindExternalFunction("Ghost", (string func) =>
            {
                switch (func)
                {
                    case "Happy":
                        ghost.sprite = happyGhost;
                        break;
                    case "Angry":
                        ghost.sprite = angryGhost;
                        break;
                }
            });
            
            story.BindExternalFunction("UnlockCategory", (int index) =>
            {
                Diary.DiaryUI.instance.UnlockCategory(index);
            });
            
            story.BindExternalFunction("Amanda", (string func) =>
            {
                Debug.Log(func);
                switch (func)
                {
                    case "Cardboard":
                        amanda.sprite = amandaWithoutCardboard;
                        amanda.GetComponent<Amanda>().withoutCardBoard = true;
                        break;
                    case "SadAmanda":
                        amanda.sprite = sadAmanda;
                        break;
                }
            });
            
            story.BindExternalFunction("AddItem", (string name, int amount) =>
            {
                var index = ItemsList.instance.datas.FindIndex(i => i.name == name);
                ItemData data = ItemsList.instance.datas[index];
                Player.Player.main.inventory.entityInventory.AddItem(data, amount);
                Debug.Log("I have to give you the " + data.name + " item");
            });

            story.BindExternalFunction("HideLilith", () =>
            {
               FindAnyObjectByType<Lilith>().Hide(); 
            });
            
        }

        public void UnBind(Story story)
        {
            story.UnbindExternalFunction("Ghost");
        }
    }
}