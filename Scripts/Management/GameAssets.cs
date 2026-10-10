using System;
using UnityEngine;

namespace Scripts
{
    public class GameAssets : MonoBehaviour
    {
        public static GameAssets instance { get; private set; }

        public Transform chatBubble;
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            instance = this;
        }

        private void Update()
        {
           // if (Input.GetKeyDown(KeyCode.S))
            //{
             //   ChatBubble.Create(Player.Player.main.transform, Vector3.zero, "spierdalaj", 10);
            //}
        }
    }
}