using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Scripts.Language
{
    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager instance { get; private set; }

        public bool pl = false;
        public bool en = false;

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            ChangeLang("en");
        }

        public void ChangeLang(string lang)
        {
            LanguageLoader.LoadLanguage(CultureInfo.GetCultureInfo(lang));

            if(lang == "en")
            {
                en = true;
                pl = false;
            }
            else if(lang == "pl")
            {
                en = false;
                pl = true;
            }
        }
    }
}