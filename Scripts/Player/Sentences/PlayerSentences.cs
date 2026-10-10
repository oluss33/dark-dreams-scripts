using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Scripts
{
    public class PlayerSentences : MonoBehaviour
    {
        [SerializeField] private Text sentencesText;
        [SerializeField] private float textSpeed;
        [SerializeField] private float timeToWait;
        public static PlayerSentences instance;
        Coroutine typingCoroutine;
        private AudioSource source;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            sentencesText.gameObject.SetActive(false);
            source = sentencesText.gameObject.GetComponent<AudioSource>();
        }

        public void SaySentence(Sentence sentence)
        {
            typingCoroutine = StartCoroutine(TypeLine(sentence.content));
            if (sentence.sentenceClip != null)
            {
                source.clip = sentence.sentenceClip;
                source.Play();
            }
        }
        
        IEnumerator TypeLine(string line)
        {
            sentencesText.text = "";
            sentencesText.gameObject.SetActive(true);
            for (int i = 0; i < line.Length; i++)
            {
                sentencesText.text += line[i];
                yield return new WaitForSeconds(textSpeed);
            }

            yield return new WaitForSeconds(timeToWait);
            
            sentencesText.gameObject.SetActive(false);
            
            typingCoroutine = null;
        }
    }
}