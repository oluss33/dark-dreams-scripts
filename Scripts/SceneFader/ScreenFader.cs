using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Scripts.Scenes
{
    public class ScreenFader : MonoBehaviour
    {
        public static ScreenFader Instance { get; private set; }
        public Animator anim;

        private int LevelInt = -1;
        private string LevelName;

        private void Start() {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else
            {
                Destroy(gameObject);
                Debug.Log("Kurwa wiecej niz 1 ScreenFasder");
            }
        }
        public void FadeToLevel(int level)
        {
            LevelInt = level;
            anim.SetTrigger("FadeOut");
        }
        public void FadeToLevelName(string name)
        {
            LevelName = name;
            anim.SetTrigger("FadeOut");
        }
        public void OnFadeEnd()
        {
            if (LevelInt == -1)
            {
                Debug.Log("Laduje scene " + LevelName);
                SceneManager.LoadScene(LevelName);
            }
            else if (LevelName == null)
            {
                SceneManager.LoadScene(LevelInt);
            }

            anim.SetTrigger("FadeIn");
            LevelInt = -1;
            LevelName = null;
        }
    }
}

