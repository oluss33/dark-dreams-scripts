using UnityEngine;

namespace Scripts { 
    public class ArenaBarrier : MonoBehaviour
    {
        private Animator anim;
        public Transform parent;
        private AudioSource source;

        private AudioClip clip;
        void Start()
        {
            anim = GetComponent<Animator>();
            source = GetComponent<AudioSource>();
            clip = Resources.Load<AudioClip>("Sounds/Worm/worm_earthquake");
        }

        public void PlaySound()
        {
            source.PlayOneShot(clip);
        }

        public void Deactivate()
        {
            anim.SetTrigger("Stop");
        }

        public void Disable()
        {
            parent.gameObject.SetActive(false);
        }

        public void Reset()
        {
            anim.Rebind();
            anim.Update(0);
        }
    }
}

