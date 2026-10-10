using System;
using UnityEngine;

namespace Scripts.Sounds
{
    public class SoundsManager : MonoBehaviour
    {
        public static AudioClip swoosh;
        
        private void Start()
        {
            swoosh = Resources.Load<AudioClip>("Sounds/swoosh_sound");
        }
    }
}
