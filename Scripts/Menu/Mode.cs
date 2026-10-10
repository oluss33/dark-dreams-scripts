using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Menu {
    public class Mode : MonoBehaviour
    {
        public void Load() {
            SceneManager.LoadScene("Intro");
        }
    }
}
