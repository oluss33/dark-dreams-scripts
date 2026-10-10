using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Scripts.Language {
    public static class Lang {

        public static bool HasKey(string key) {
            return LanguageLoader.Current.HasKey(key);
        }

        public static string Get(string key, params object[] formatArgs) {
			return LanguageLoader.Current.Get(key, formatArgs);
		}
    }
}