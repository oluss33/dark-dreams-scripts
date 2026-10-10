using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Video;

namespace Scripts
{
    public class Cutscene : MonoBehaviour
    {
        [SerializeField] VideoPlayer player;
        [SerializeField] GameObject fadeIn;

        private void Start()
        {
            Play();
            StartCoroutine(Time());
        }

        public void Play()
        {
            player.Play();
        }
        
        IEnumerator Time()
        {
            yield return new WaitForSeconds(16);
            fadeIn.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Credits");
        }
    }
}