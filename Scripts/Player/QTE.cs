using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Scripts.Player
{
    public class QTE : MonoBehaviour
    {
        public static QTE instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        public void StartQTE()
        {
            
        }
        
        
    }
}