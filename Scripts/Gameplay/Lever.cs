using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay
{
    public class Lever : MonoBehaviour
    {
        public GameObject objectToOpen;
        bool isOpened = false;

        Animator anim;

        AudioSource source;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            source = GetComponent<AudioSource>();
        }

        void OnMouseDown()
        {

            if (!isOpened)
            {
                anim.SetBool("Static", false);
                anim.SetBool("LeverUsed", true);
            }
            else
            {
                anim.SetBool("LeverUsed", false);
            }

            source.Play();
            objectToOpen.SetActive(!objectToOpen.activeSelf);
            isOpened = !isOpened;
        }
    }
}
