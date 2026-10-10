using System.Collections;
using System.Collections.Generic;
using Scripts.Menu;
using Scripts.Player.Movement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class Pause : MonoBehaviour
    {
        public static bool isGamePaused = false;

        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject options;

        [Header("Buttons")] 
        [SerializeField] private GameObject[] buttons;

        [Header("Option Buttons")]
        [SerializeField] private GameObject backBtn;
        
        bool optionsOpened = false;
        Player.Player player;
        bool canResume = true;


        void Start()
        {
            pauseMenu.SetActive(false);
            options.SetActive(false);
            player = Player.Player.main;
            PlayerController.instance.controls.Player.Pause.performed += ctx => Check();
        }

        void Check()
        {
            if (Player.Player.main.died) return;
            if (Diary.DiaryUI.instance.isOpened)
            {
                Diary.DiaryUI.instance.Open();
                return;
            }
            if (isGamePaused && !optionsOpened && canResume)
            {
                ResumeGame();
            }
            else if (!isGamePaused && !optionsOpened)
            {
                PauseGame();
                EventSystem.current.SetSelectedGameObject(buttons[0]);
                buttons[0].GetComponent<MenuButton>().elements.SetActive(true);
                Debug.Log(EventSystem.current.gameObject.name);
            }

            if (!isGamePaused && optionsOpened)
            {
                Back();
            } 
            
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void OpenOptions()
        {
            options.SetActive(true);
            pauseMenu.SetActive(false);
            optionsOpened = true;
            
            EventSystem.current.SetSelectedGameObject(backBtn);
            backBtn.GetComponent<MenuButton>().elements.SetActive(true);
        }

        public void Back()
        {
            options.SetActive(false);
            pauseMenu.SetActive(true);
            optionsOpened = false;
            
            EventSystem.current.SetSelectedGameObject(buttons[0]);
            buttons[0].GetComponent<MenuButton>().elements.SetActive(true);
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }
        void PauseGame()
        {
            pauseMenu.SetActive(true);
            
            
            Time.timeScale = 0f;
            isGamePaused = true;
        }
    }
}