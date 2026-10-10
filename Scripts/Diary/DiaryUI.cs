using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Player.Movement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace Scripts.Diary
{
    public class DiaryUI : MonoBehaviour, Openable
    {
        private int index = 0;
        private int maxIndex = -1;

        public static DiaryUI instance;
        public DiaryInputActions input;

        [SerializeField] private GameObject[] UIs;
        [SerializeField] private GameObject playerUI;
        private GameObject skillsUI, questsUI, locationsUI, charactersUI, inventoryUI, craftingUI;
        public Button[] buttons;
        public bool isDiaryUnlocked = false;
        public bool[] categories_unlocked = { false, false, false, false, false, false };
        public bool isOpened = false;

        /*     [SerializeField] GameObject skillsUI;
             [SerializeField] GameObject questsUI;
             [SerializeField] GameObject playerUI;
             [SerializeField] GameObject locationsUI;
             [SerializeField] GameObject charactersUI;
             [SerializeField] GameObject inventoryUI;
             [SerializeField] public bool isOpened = false;*/
        public bool IsOpened()
        {
            return isOpened;
        }

        private void Awake()
        {
            instance = this;

            input = new DiaryInputActions();
            
            input.Enable();

            input.DiaryUI.Left.performed += ctx => Move(0);
            input.DiaryUI.Right.performed += ctx => Move(1);

            Transform bg = playerUI.transform.Find("Background");
            skillsUI = bg.Find("SkillTreeUI").gameObject;
            inventoryUI = bg.Find("Inventory").gameObject;
            questsUI = bg.Find("QuestsUI").gameObject;
            locationsUI = bg.Find("LocationsUI").gameObject;
            charactersUI = bg.Find("CharactersUI").gameObject;
            craftingUI = bg.Find("Crafting").gameObject;

            maxIndex = UIs.Length;
        }


        void Move(int direction = 1 /* 0 - left,  1 - right*/)
        {
            if (direction == 1 && index < maxIndex)
            {
                index++;
            }
            else if (direction == 0 && index > 0)
            {
                index--;
            }

            GameObject g = UIs[index];
            
            if(g == skillsUI) Skills();
            if (g == inventoryUI) Inventory();
            if (g == questsUI) Quests();
            if(g == locationsUI) Locations();
            if (g == charactersUI) Characters();
        }

        void Start()
        {
            Inventory();
            playerUI.SetActive(false);
            PlayerController.instance.controls.Player.CharacterMenu.performed += ctx => Open();
        }


        void DisableUI()
        {
            playerUI.SetActive(false);
        }

        void Pause()
        {
            Time.timeScale = 0f;

        }

        public void UnlockCategory(int index)
        {
            if (isDiaryUnlocked == false) isDiaryUnlocked = true;
            categories_unlocked[index] = true;
        }

        public void Open()
        {
            if (!isDiaryUnlocked) return;
            print("probuje otworzyc diary");
            // if (Objects.ChestPassword.instance.opened) return;
            Openable[] openables = GameObject.FindObjectsByType<MonoBehaviour>(0, 0).OfType<Openable>().ToArray();
            foreach (Openable openable in openables)
            {
                if (openable.IsOpened() && openable != this)
                {
                    print("wypierdalam z petli");
                    return;
                }
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = categories_unlocked[i];
            }
            
            playerUI.GetComponent<AudioSource>().Play();
            if (isOpened)
            {
                Time.timeScale = 1f;
                LeanTween.moveLocalY(playerUI, -450, 0.15f).setOnComplete(DisableUI);
            }

            if (!isOpened)
            {
                playerUI.SetActive(true);
                LeanTween.moveLocalY(playerUI, 0, 0.15f).setOnComplete(Pause);
            }

            isOpened = !isOpened;
            questsUI.GetComponent<Quest.QuestsUI>().UpdateQuests();
        }

        void Exit()
        {
            skillsUI.SetActive(false);
            questsUI.SetActive(false);
            locationsUI.SetActive(false);
            charactersUI.SetActive(false);
            inventoryUI.SetActive(false);
            craftingUI.SetActive(false);
        }


        public void Skills()
        {
            skillsUI.SetActive(true);
            questsUI.SetActive(false);
            locationsUI.SetActive(false);
            charactersUI.SetActive(false);
            inventoryUI.SetActive(false);
            craftingUI.SetActive(false);

        }

        public void Quests()
        {
            skillsUI.SetActive(false);
            questsUI.SetActive(true);
            questsUI.GetComponent<Quest.QuestsUI>().UpdateQuests();
            locationsUI.SetActive(false);
            charactersUI.SetActive(false);
            inventoryUI.SetActive(false);
            craftingUI.SetActive(false);

        }

        public void Locations()
        {
            skillsUI.SetActive(false);
            questsUI.SetActive(false);
            locationsUI.SetActive(true);
            charactersUI.SetActive(false);
            inventoryUI.SetActive(false);
            craftingUI.SetActive(false);

        }

        public void Characters()
        {
            skillsUI.SetActive(false);
            questsUI.SetActive(false);
            locationsUI.SetActive(false);
            charactersUI.SetActive(true);
            inventoryUI.SetActive(false);
            craftingUI.SetActive(false);

        }

        public void Inventory()
        {
            skillsUI.SetActive(false);
            questsUI.SetActive(false);
            locationsUI.SetActive(false);
            charactersUI.SetActive(false);
            inventoryUI.SetActive(true);
            craftingUI.SetActive(false);

        }
        
        public void Crafting()
        {
            skillsUI.SetActive(false);
            questsUI.SetActive(false);
            locationsUI.SetActive(false);
            charactersUI.SetActive(false);
            inventoryUI.SetActive(false);
            craftingUI.SetActive(true);

        }

        private void OnDisable()
        {
            input.Disable();
        }
    }
}