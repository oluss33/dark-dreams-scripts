using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;
using Scripts.Scenes;

namespace Scripts.Language {
    public class LanguageSelector : MonoBehaviour
    {

        public MenuInputActions actions;

        void Start()
        {
            actions = new MenuInputActions();
            actions.Enable();
                    actions.Menu.Continue.performed += ctx => Continue();
        }

        void Continue()
        {
            actions.Disable();
          // SceneManager.LoadScene("Menu");
          ScreenFader.Instance.FadeToLevelName("Menu");
        }

        public void SelectLang(Dropdown dropdown) {
            if(dropdown.value == 0) LanguageManager.instance.ChangeLang("en");
            if(dropdown.value == 1) LanguageManager.instance.ChangeLang("pl");
            LanguageUpdater.Instance.UpdateUI();
        }

        void OnDisable() {
            actions.Disable();
        }
    }
}
