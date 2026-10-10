using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Menu {
    public class ModeLoader : MonoBehaviour
    {
        public string multi;
        public string solo;

        public void Multiplayer()
        {
            SceneManager.LoadScene(multi);
        }

        public void Solo()
        {
            SceneManager.LoadScene(solo);
        }
    }
}
