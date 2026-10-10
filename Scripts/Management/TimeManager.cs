using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Management
{
    public class TimeManager : MonoBehaviour
    {
        
        public static TimeManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        public void DoSlowMotion(float timeScale, float duration)
        {
            StartCoroutine(SlowMotion(timeScale, duration));
        }

        private IEnumerator SlowMotion(float timeScale, float duration)
        {
            print("perform slowMotion");
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = 0.02f * timeScale;

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}