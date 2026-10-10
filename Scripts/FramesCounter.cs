using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class FramesCounter : MonoBehaviour
    {
        [SerializeField] Text fpsText;
        [SerializeField] int poolingTime;
        float time;
        int frameCount;

        private void Update()
        {
            time += Time.deltaTime;
            frameCount++;
            if(time >= poolingTime)
            {
                int frameRate = Mathf.RoundToInt(frameCount / time);
                fpsText.text = frameRate.ToString() + " FPS";
                time -= poolingTime;
                frameCount = 0;
            }
        }
    }
}