using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Scripts.Language {
	public sealed class Language {

		private Dictionary<string, string> lines;

		public Language(Dictionary<string, string> lines) {
			this.lines = lines;
		}

		public string Get(string key, params object[] objectArgs) {
			return string.Format(GetWithAppliedDataBase(key), objectArgs);
		}

		public bool HasKey(string key) {
			return lines.ContainsKey(key);
		}
		
		public String GetRaw(string key) {
			return lines[key];
		}
		
		private string GetWithAppliedDataBase(string key) {
			return KeyValueDatabase.ApplyTo(GetRaw(key));
		}
	}

	public static class KeyValueDatabase {
		private static Regex searchRegex = new Regex(@"\{\{(\w+)\}\}", RegexOptions.Compiled);
		private static readonly Dictionary<string, string> valueDictionary = new Dictionary<string, string>();
		
		
		public static void Set(string key, string value) {
			valueDictionary[key] = value;
		}

		public static void Remove(string key) {
			valueDictionary.Remove(key);
		}

		public static string Get(string key) {
			if(!valueDictionary.ContainsKey(key)) return "undefined";
			return valueDictionary[key];
		}

		public static string ApplyTo(string text) {
			return searchRegex.Replace(text, match => Get(match.Groups[1].Value));
		}
	}
}