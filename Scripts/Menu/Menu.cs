using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Scripts.Scenes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Scripts.Menu
{
    public class Menu : MonoBehaviour
    {

        public GameObject welcomePanel;
        
        public GameObject options;
        public GameObject menu;
        public GameObject ambient;
        public GameObject playUI;
        public GameObject conceptsUI;

        public GameObject languageUI;

        private MenuInputActions actions;

        private void Awake()
        {
            actions = new MenuInputActions();
            actions.Enable();
       //     actions.Menu.Continue.performed += ctx => Continue();
            actions.Menu.Back.performed += ctx => BackOptions();
        }

        void BackOptions()
        {
            if (options.activeSelf)
            {
                Back();
            }
        }

        public void Continue()
        {
            if (welcomePanel.activeInHierarchy)
            {
                welcomePanel.SetActive(false);
                languageUI.SetActive(true);
            }
            else
            {
                languageUI.SetActive(false);
                menu.SetActive(true);
            }
        }
        void Start()
        {
            menu.SetActive(true);
            options.SetActive(false);
            playUI.SetActive(false);
            conceptsUI.SetActive(false);
          //  languageUI.SetActive(true);
           // welcomePanel.SetActive(true);
            
            if(FindObjectOfType<Ambient>() == null)
            {
                Instantiate(ambient);
            }
        }

        public void Options()
        {
            menu.SetActive(false);
            options.SetActive(true);
        }

        public void Back()
        {
            menu.SetActive(true);
            options.SetActive(false);
            playUI.SetActive(false);
            conceptsUI.SetActive(false);
        }

        public void PlayGame()
        {
            playUI.SetActive(true);
            menu.SetActive(false);
        }

        public void ConceptArts()
        {
            menu.SetActive(false);
            conceptsUI.SetActive(true);
        }

        public void PlaySolo()
        {
            //SceneManager.LoadScene("Intro");
            actions.Disable();
            //ScreenFader.Instance.FadeToLevelName("Intro");
            SceneManager.LoadScene("Intro");
        }

        public void Quit()
        {
            Application.Quit();
            //if (Language.LanguageManager.instance.en)
            //{
            //    QuestionDialog.instance.ShowQuestion("Are you really want to exit the game?",
            //    () =>
            //    {
            //        Application.Quit();
            //    },
            //    () =>
            //    {
            //        QuestionDialog.instance.Hide();
            //    });
            //}
            //else if(Language.LanguageManager.instance.pl)
            //{
            //    QuestionDialog.instance.ShowQuestion("Czy na pewno chcesz wyjść z gry?",
            //    () =>
            //    {
            //        Application.Quit();
            //    },
            //    () =>
            //    {
            //        QuestionDialog.instance.Hide();
            //    });
            //}
        }
    }
}
