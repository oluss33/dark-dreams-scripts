using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Language {
    [DefaultExecutionOrder(-102384)]
    public class AutoTranslate : MonoBehaviour
    {
        [SerializeField] protected string key;

        public virtual void UpdateText() {
            if(LanguageLoader.Current == null) return;
            if(!Lang.HasKey(key)) { Debug.Log("gowno", this); return; }
            if(GetComponent<Text>() == null) Debug.Log(gameObject.name);
            GetComponent<Text>().text = Lang.Get(key);
        }
    }

    public class AutoTranslatePro : AutoTranslate {
        public override void UpdateText()
        {
            if(!Lang.HasKey(key)) { Debug.Log("gowno", this); return; }
            GetComponent<TextMesh>().text = Lang.Get(key);
        }
    }
}
