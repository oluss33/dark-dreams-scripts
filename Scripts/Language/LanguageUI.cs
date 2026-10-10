using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Language {
    public class LanguageUI : MonoBehaviour
    {
        
        void Awake() {
        }

        void Start() {
            TranslateLang();
        }

        public void TranslateLang() {
            AutoTranslate[] textArray = GetComponentsInChildren<AutoTranslate>();
            foreach(AutoTranslate text in textArray) {
                text.UpdateText();
            }
        }
    }
}
