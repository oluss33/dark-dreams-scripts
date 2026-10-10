using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    namespace Scripts.Language {
    [DefaultExecutionOrder(-102384)]
    public class LanguageUpdater : MonoBehaviour
    {
        public static LanguageUpdater Instance;
        LanguageUI[] uIs;
        void Awake() {
            if(Instance == null) Instance = this;
            else return;

            uIs = FindObjectsOfType<LanguageUI>();
        }

        void Start() {
            UpdateUI();
        }

        public void UpdateUI() {
            foreach(LanguageUI lang in uIs) {
                lang.TranslateLang();
            }
        }
    }
}
