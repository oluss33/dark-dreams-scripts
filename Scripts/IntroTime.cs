using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Scripts {
    public class IntroTime : MonoBehaviour
    {
        [SerializeField]
        private float delayBeforeLoading = 10f;
        [SerializeField]
        private string sceneNameToLoad;
        [SerializeField] GameObject player;
        [SerializeField] GameObject music;
        private float timeElapsed;
        IntroInputActions inputActions;

        public Text subText;

        void Start() {
            StartCoroutine(Seconds());
            inputActions = new IntroInputActions();
            inputActions.Enable();
            inputActions.Intro.Skip.performed += ctx => Skip();
            Destroy(FindObjectOfType<Ambient>().gameObject);
            Cursor.visible = false;
            subText.gameObject.SetActive(false);
            if (Language.LanguageManager.instance.en)
                subText.text = "Once upon a time there was a boy called Alten. He led a calm life, but on one stormy night everything changed.";
            if(Language.LanguageManager.instance.pl)
                subText.text = "Pewnego razu byl sobie chlopiec o imieniu Alten. Prowadzil spokojne zycie, dopoki jedna burzowa noc wszystkiego nie zmienila.";
        }

        IEnumerator Seconds() {
            subText.gameObject.SetActive(false);
            yield return new WaitForSeconds(7f);
            player.GetComponent<VideoPlayer>().Play();
            music.GetComponent<AudioSource>().Play();
            subText.gameObject.SetActive(true);
            StartCoroutine(Subs());
        }

        IEnumerator Subs()
        {
            if (Language.LanguageManager.instance.en)
            {
                yield return new WaitForSeconds(10);
                subText.text = "Alten woke up when the clock struck midnight. Boy looked around and thought, he would try to fall asleep again,";
                yield return new WaitForSeconds(8);
                subText.text = "but a noise coming outside the door has interrupted him.";
                yield return new WaitForSeconds(5);
                subText.text = "He was frightened, but decided to check what makes those sound anyway.";
                yield return new WaitForSeconds(6);
                subText.text = "Alten opened the door and saw a silhouette, but couldn't figure out what was that.";
                yield return new WaitForSeconds(5);
                subText.text = "Suddenly, everything went dark and Alten lost consciousness.";
                yield return new WaitForSeconds(5);
                subText.text = "After some time, he woke up in a foreign place. Everything around was like bigger than usual.";
                yield return new WaitForSeconds(7);
                subText.text = "Alten didn't know where they was right now, but knew that he should get out of here as soon as possible.";
             }
            if (Language.LanguageManager.instance.pl)
            {
                yield return new WaitForSeconds(10);
                subText.text = "Alten obudzil sie kiedy wybila polnoc. Rozejrzal sie dookola, po czym znowu sprobowal zasnac,";
                yield return new WaitForSeconds(8);
                subText.text = "ale przerwal mu halas dochodzacy zza drzwi.";
                yield return new WaitForSeconds(5);
                subText.text = "Byl wystraszony, ale zdecydowal sprawdzic co tak halasuje.";
                yield return new WaitForSeconds(6);
                subText.text = "Alten otworzyl drzwi i zobaczyl pewna postac, ale nie mogl zrozumiec, co to bylo.";
                yield return new WaitForSeconds(5);
                subText.text = "Nagle wszystko sciemnialo i Alten stracil przytomnosc.";
                yield return new WaitForSeconds(5);
                subText.text = "Po pewnym czasie obudzil sie w obcym miejscu. Wszystko wokol bylo wieksze niz dotychczas.";
                yield return new WaitForSeconds(7);
                subText.text = "Alten nie wiedzial gdzie sie znajdowal, ale byl pewny, ze musi uciec stad najszybciej jak to mozliwe.";
            }
        }

        void Skip()
        {
            Cursor.visible = true;
            inputActions.Disable();
            SceneManager.LoadScene(sceneNameToLoad);
        }

        private void Update()
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > delayBeforeLoading)
            {
                Cursor.visible = true;
                SceneManager.LoadScene(sceneNameToLoad);
            }
        }
    }
}
