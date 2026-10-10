using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace Scripts.Language.Editor {
	public class LangFilePreprocessor : AssetPostprocessor {
		/*
		 * Key structure:
		 * - Only lowercase
		 * - charset: a-z 0-9 _ .
		 *
		 * Key to variable conversion
		 * - upper first letter
		 * - replace '_' with upper letter
		 * - replace '.' with '_'
		 *
		 * Example:
		 * - key: npc.shop.welcome_dialogue.status_happy
		 *   var: Npc_Shop_WelcomeDialogue_StatusHappy
		 */
		
		
		public static readonly string LangKeyDatabaseNamespace = "Scripts.Language"; 
		public static readonly string LangKeyDatabaseClassName = "K";
		public static readonly string LangKeyDatabasePath = "Assets/Scripts/Language/"+LangKeyDatabaseClassName+".cs";
		
		private static readonly string FieldStructurePrefix = "\t\tpublic static readonly string ";
		
		public static void OnPostprocessAllAssets(
			string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
			string[] langFiles = FilterLanguage(importedAssets);
			if (langFiles.Length > 0) {
				ProcessLangFiles(langFiles);
			}
		}
		
		private static string[] FilterLanguage(string[] assets) {
			return assets
				.Where(asset => asset.StartsWith(LanguageLoader.languageFilesPath))
				.Where(asset => asset.EndsWith(".xml"))
				.ToArray();
		}

		private static void ProcessLangFiles(string[] fileArray) {
			// list of keys of every file
			List<HashSet<string>> keySetList = LoadKeySetList(fileArray);
			if (!ValidateKeySets(keySetList)) {
				Debug.LogError("Lang files are different! Please check language files and fix missing keys!");
			}
			
			HashSet<string> keys = keySetList[0];
			if (IsClassRegenerationNeeded(keys)) {
				RegenerateClass(keys);
			}
		}
		

		private static bool ValidateKeySets(List<HashSet<string>> keySetList) {
			HashSet<string> first = keySetList[0];
			// This loop can be slow...
			for (int i = 1; i < keySetList.Count; i++) {
				if (!first.Equals(keySetList[i])) {
					return false;
				}
			}

			return true;
		}

		private static List<HashSet<string>> LoadKeySetList(string[] fileArray) {
			List<HashSet<string>> keySetList = new List<HashSet<string>>(fileArray.Length);

			foreach (string file in fileArray) {
				HashSet<string> keySet = ReadKeysFromLangFile(file);
				if (keySet.Count == 0) {
					continue;
				}
				
				keySetList.Add(keySet);
			}

			return keySetList;
		}

		private static HashSet<string> ReadKeysFromLangFile(string file) {
			XmlDocument document = new XmlDocument();
			document.LoadXml(File.ReadAllText(file));
			XmlNodeList translationsNodeList = document.SelectNodes("/translations/line");
			if (translationsNodeList == null) {
				return new HashSet<string>();
			}

			HashSet<string> keySet = new HashSet<string>();
			foreach(XmlNode line in translationsNodeList) {
				string key = ReadAttribute(line);
				if (key == null) {
					continue;
				}

				keySet.Add(key);
			}

			return keySet;
		}

		private static string ReadAttribute(XmlNode line) {
			XmlAttribute keyAttribute = line.Attributes["key"];
			return keyAttribute?.Value;
		}

		private static void RegenerateClass(HashSet<string> keySet) {
			if (File.Exists(LangKeyDatabasePath)) {
				File.Delete(LangKeyDatabasePath);
			}

			StringBuilder fileBuilder = new StringBuilder();

			fileBuilder.Append($"namespace {LangKeyDatabaseNamespace} {{\n");

			fileBuilder.Append($"\tpublic static class {LangKeyDatabaseClassName} {{\n");
			foreach (string key in keySet) {
				string variable = ConvertKeyToVariableName(key);
				fileBuilder.Append(FieldStructurePrefix + variable + " = \"" + key + "\";\n");	
			}

			fileBuilder.Append("\t}\n");
			fileBuilder.Append("}");
			
			FileStream fileStream = File.Create(LangKeyDatabasePath);
			byte[] data = Encoding.ASCII.GetBytes(fileBuilder.ToString());
			fileStream.Write(data, 0, data.Length);
			fileStream.Close();
		}

		private static string ConvertKeyToVariableName(string key) {
			char[] chars = key.ToLower().ToCharArray();
			chars[0] = char.ToUpper(chars[0]);
			for (int i = 1; i < chars.Length - 1; i++) {
				if (chars[i] == '_' || chars[i] == '.') {
					chars[i + 1] = char.ToUpper(chars[i + 1]);
				}
			}

			return new string(chars).Replace("_", "").Replace('.', '_');
		}

		private static bool IsClassRegenerationNeeded(HashSet<string> keys) {
			if (!ClassExists()) {
				return true;
			}

			string classContent = LoadClassContent();
			HashSet<string> alreadyCreatedKeySet = ReadKeysFromClass(classContent);
			if (alreadyCreatedKeySet.Equals(keys)) {
				return false;
			}

			return true;
		}

		private static HashSet<string> ReadKeysFromClass(string classContent) {
			string namespaceLine = $"namespace {LangKeyDatabaseNamespace} {{\n";
			int from = classContent.IndexOf('{', namespaceLine.Length) + 1;
			int to = classContent.LastIndexOf('}', classContent.Length - 2) - 1;
			string fieldsContent = classContent.Substring(from, to - from);
			string[] fieldsLineArray = fieldsContent.Split('\n');
			HashSet<string> keySet = new HashSet<string>();
			foreach (string fieldLine in fieldsLineArray) {
				if (!fieldLine.StartsWith(FieldStructurePrefix)) { 
					continue;
				}

				if (!fieldLine.EndsWith("\";")) { // ";
					continue;
				}

				int keyBegin = fieldLine.IndexOf("\"");
				int keyEnd = fieldLine.Length - 2; // ";
				string key = fieldLine.Substring(keyBegin, keyEnd - keyBegin);
				keySet.Add(key);
			}

			return keySet;
		}

		private static string LoadClassContent() {
			return File.ReadAllText(LangKeyDatabasePath);
		}

		private static bool ClassExists() {
			return File.Exists(LangKeyDatabasePath);
		}

	}
}