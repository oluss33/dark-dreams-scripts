using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Menu {
    public class Credits : MonoBehaviour
    {

        public GameObject logo;
        public GameObject text1;

        void Start() {
            StartCoroutine(Logo());
            Destroy(FindObjectOfType<Music.Soundtrack>());
        }

        IEnumerator Logo() {
            logo.SetActive(true);
            LeanTween.alphaCanvas(logo.GetComponent<CanvasGroup>(), 1f, 2f);
            yield return new WaitForSeconds(6f);
            LeanTween.alphaCanvas(logo.GetComponent<CanvasGroup>(), 0f, 2f);
            yield return new WaitForSeconds(2f);
            LeanTween.alphaCanvas(text1.GetComponent<CanvasGroup>(), 1f, 2f);
            yield return new WaitForSeconds(2f);
            LeanTween.alphaCanvas(text1.GetComponent<CanvasGroup>(), 0f, 2f);
            logo.SetActive(false);
        }

        IEnumerator Exit()
        {
            yield return new WaitForSeconds(60);
            SceneManager.LoadScene("Menu");
        }

        void Update() {
            if(Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
