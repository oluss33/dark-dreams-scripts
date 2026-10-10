using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class QuestionDialog : MonoBehaviour
    {
        public static QuestionDialog instance { get; private set; }

        Text text;
        Button yesBTN;
        Button noBTN;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            text = transform.Find("Text").GetComponent<Text>();
            yesBTN = transform.Find("YesBTN").GetComponent<Button>();
            noBTN = transform.Find("NoBTN").GetComponent<Button>();
            Hide();
        }

        public void ShowQuestion(string questionText, Action yesAction, Action noAction)
        {
            gameObject.SetActive(true);
            text.text = questionText;
            yesBTN.onClick.AddListener(() =>
            {
                Hide();
                yesAction();
            });
            noBTN.onClick.AddListener(() =>
            {
                Hide();
                noAction();
            });
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
