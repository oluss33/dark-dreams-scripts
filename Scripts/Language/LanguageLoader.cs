using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Scripts.Language {
    public static class LanguageLoader
    {
            public static Language Current { get; private set; }

            public static readonly string languageFilesPath = @"Languages/"; 
            
            public static void LoadLanguage(CultureInfo language) {
                string languageCode = language.TwoLetterISOLanguageName;
                TextAsset xmlAsset = GetLanguageTextAssetFor(languageCode);
                if (!xmlAsset) {
                    Debug.Log("There is no language for " + languageCode 
                            + ". Not found file: " + GetLanguageAssetFilePath(languageCode));
                    return;
                }

                Dictionary<string, string> lineDictionary = ProcessXml(xmlAsset);
                Current = new Language(lineDictionary);
            }

            private static TextAsset GetLanguageTextAssetFor(String languageCode) {
                string path = GetLanguageAssetFilePath(languageCode);
                return Resources.Load<TextAsset>(path);
            }

            private static string GetLanguageAssetFilePath(string languageCode) {
                string name = languageCode;
                string fileName = "lang_" + name;
                return Path.Combine(languageFilesPath, fileName);
            }

            private static Dictionary<string, string> ProcessXml(TextAsset textAsset) {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(textAsset.text);
                XmlNodeList translationsNodeList = xmlDocument.SelectNodes("/translations/line");
                if (translationsNodeList == null) {
                    return new Dictionary<string, string>(0);
                }

                Dictionary<string, string> lineDictionary = new Dictionary<string, string>(translationsNodeList.Count);
                foreach(XmlNode line in translationsNodeList) {
                    (string key, string value) = ProcessLineNode(line);
                    if (key == null) {
                        continue;
                    }

                    lineDictionary[key] = value;
                }

                return lineDictionary;
            }

            private static (string, string) ProcessLineNode(XmlNode lineNode) {
                XmlAttribute keyAttribute = lineNode.Attributes["key"];
                if (keyAttribute == null) {
                    return (null,null);
                }

                string key = keyAttribute.Value;
                string value = lineNode.InnerText;
                return (key, value);
            }

        }
    }
