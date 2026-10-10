using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Sounds
{
    public class PlayerSounds : MonoBehaviour
    {
        public static PlayerSounds instance { get; private set; }

        public  AudioClip[] dyingSounds;
        public  AudioClip[] jumpSounds;

        private AudioSource _source;
        
        private void Awake()
        {
            instance = this;
            GetDyingSounds();
        }

        private void Start()
        {
            _source = Player.Player.main.source;
        }

        private void GetDyingSounds()
        {
            dyingSounds = Resources.LoadAll<AudioClip>("Sounds/Player/FINAL/dying_clips");
        }

        public void PlayJumpSound()
        {
            AudioClip jumpSound = jumpSounds[Random.Range(0, jumpSounds.Length)];
            _source.PlayOneShot(jumpSound);
        }
    }
}