using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Music
{
    public class Soundtrack : MonoBehaviour
    {
        public AudioClip[] clips;
        public AudioClip curClip;
        public AudioSource source;
        AudioClip lastClip;
        List<AudioClip> usedClips = new List<AudioClip>();
        public static Soundtrack instance;

        public bool slowlyMute = false;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            source = GetComponent<AudioSource>();
            source.loop = false;
        }

        AudioClip GenerateClips()
        {
            curClip = clips[Random.Range(0, clips.Length)];
            while (usedClips.Contains(curClip)) {
                curClip = clips[Random.Range(0, clips.Length)];
            }
            return curClip;
        }

        void Update()
        {
            if (!source.isPlaying)
            {
                if (source.clip = lastClip) source.clip = GenerateClips();
                source.clip = GenerateClips();
                source.Play();
                lastClip = source.clip;
            }

            if(source.isPlaying)
            {
                if(slowlyMute)
                {
                    source.volume -= 1 * Time.deltaTime;
                }
            }
        }
    }
}
