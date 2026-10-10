using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts {
    public class Screenshot : MonoBehaviour
    {
        int superSize = 2;
        int shotIndex = 0;
        void Update() {
            if(Input.GetKeyDown(KeyCode.G)) {
                ScreenCapture.CaptureScreenshot($"Screenshot{shotIndex}.png", superSize);
                shotIndex++;
            }
        }
    }
}