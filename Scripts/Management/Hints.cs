using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    public class Hints : MonoBehaviour
    {
        public static Hints instance { get; private set; }

        public Transform hintsUI;

        Text titleText;
        Text contentText;

        private AudioSource source;
        private Animator anim;

        public void Start()
        {
            instance = this;
            titleText = hintsUI.Find("Title").GetComponent<Text>();
            contentText = hintsUI.Find("Text").GetComponent<Text>();
            source = hintsUI.GetComponent<AudioSource>();
            hintsUI.gameObject.SetActive(false);
            anim = hintsUI.GetComponent<Animator>();
        }

        public void ShowHint(GameObject sender, string title, string content, float time)
        {
            print("wywoluje hint " + title + ", " + content + " z obiektu: " + sender.name);
            hintsUI.gameObject.SetActive(true);

            titleText.text = title;
            contentText.text = content;
            
            titleText.gameObject.SetActive(true);
            contentText.gameObject.SetActive(true);
            source.Play();
            anim.Play("show_hint");
            StartCoroutine(HideHint(time));
        }

        IEnumerator HideHint(float time)
        {
            yield return new WaitForSeconds(time);
            source.Play();
            anim.Play("hide_hint");
        }
    }        
}
