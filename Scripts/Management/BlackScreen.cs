using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts {
    public class BlackScreen : MonoBehaviour
    {

        public static BlackScreen instance { get; private set; }
        [SerializeField] GameObject blackScreen;

        private void Awake()
        {
            instance = this;
            blackScreen.SetActive(false);
        }

        public IEnumerator TurnBlackScreen(float time)
        {
            //Player.PlayerController.instance.enabled = false;
            blackScreen.SetActive(true);
            yield return new WaitForSeconds(time);
            blackScreen.SetActive(false);
           // Player.PlayerController.instance.enabled = true;
        }
    }
}
