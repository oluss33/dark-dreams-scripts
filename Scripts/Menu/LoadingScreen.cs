using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace Scripts.Menu {
    public class LoadingScreen : MonoBehaviour
    {
       // public GameObject loadingScreen;
       // public Text progressText;
        public Text tipsText;
        public string[] en_TipsArray;
        public string[] pl_TipsArray;
        public int tipsCount;
        public bool showOnStart;
        public bool showTips = false;
        public bool loadOnStart = false;
        public static LoadingScreen instance;
        public string sceneName;
        public Image fillArea;
        // public Text progressText;
        string lastTip;

        void Awake() {
            if(instance == null) instance = this;
            else Destroy(this);
        }

        void Start() {
            StartCoroutine(GenerateTips());
            if (loadOnStart) {
                 StartCoroutine(LoadAsync(sceneName));
            }
        }

        public void Load(int index) {
            StartCoroutine(LoadAsync(sceneName));
        }

        IEnumerator LoadAsync(string name) {
            AsyncOperation op = SceneManager.LoadSceneAsync(name);
            StartCoroutine(GenerateTips());
            while(!op.isDone) {
                float progress = Mathf.Clamp01(op.progress / 0.9f);
                fillArea.fillAmount = progress;
               // progressText.text = progress * 100f + "%";
                yield return null;
            }
        }

        IEnumerator GenerateTips() {
            if(showTips) {
                if (Language.LanguageManager.instance.en)
                {
                    tipsCount = Random.Range(0, en_TipsArray.Length);
                    if (en_TipsArray[tipsCount] == lastTip) //tipsCount = Random.Range(0, tipsArray.Length);  StartCoroutine(GenerateTips());
                        tipsText.text = en_TipsArray[tipsCount];
                    lastTip = tipsText.text;
                    yield return new WaitForSeconds(5f);
                    tipsCount++;
                    if (tipsCount >= en_TipsArray.Length)
                    {
                        tipsCount = 0;
                    }
                    StartCoroutine(GenerateTips());
                    tipsText.text = en_TipsArray[tipsCount];
                }
                if(Language.LanguageManager.instance.pl)
                {
                    tipsCount = Random.Range(0, pl_TipsArray.Length);
                    if (pl_TipsArray[tipsCount] == lastTip) //tipsCount = Random.Range(0, tipsArray.Length);  StartCoroutine(GenerateTips());
                        tipsText.text = pl_TipsArray[tipsCount];
                    lastTip = tipsText.text;
                    yield return new WaitForSeconds(5f);
                    tipsCount++;
                    if (tipsCount >= pl_TipsArray.Length)
                    {
                        tipsCount = 0;
                    }
                    StartCoroutine(GenerateTips());
                    tipsText.text = pl_TipsArray[tipsCount];
                }
            }
            else
            {
                Debug.Log("@SceneLoader: Tips disabled!");
            }
        }
        
    }
}
