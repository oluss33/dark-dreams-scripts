using System;
using UnityEngine;

namespace Scripts
{
    public class HintsTrigger : MonoBehaviour
    {
        public string title;
        public string content;
        public float time;

        private bool used = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (used) return;
            print("wywoluje hint " + title + ", " + content + " z obiektu: " + gameObject.name);
            Hints.instance.ShowHint(gameObject, title, content, time);
            used = true;
        }
    }
}