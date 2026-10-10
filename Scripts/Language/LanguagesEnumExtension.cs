using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Language {
    public enum LanguagesEnum {
        EN, PL
    }
    public static class LanguagesEnumExtension
    {
        private static Dictionary<LanguagesEnum, string> enumToLangCode = new Dictionary<LanguagesEnum, string>() {
            { LanguagesEnum.EN, "en" },
            { LanguagesEnum.PL, "pl" },
        };

        public static string ToLangCode(this LanguagesEnum lang) {
            if(enumToLangCode.ContainsKey(lang)) {
                return enumToLangCode[lang];
            }

            return "";
        }
    }
}
