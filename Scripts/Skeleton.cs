using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Skeleton
{
    public class Skeleton : MonoBehaviour
    {
        public GameObject letter;
        bool opened = false;
        AudioSource audioSource;
        Player.Player player;
        public Sprite polishSprite;
        public Sprite englishSprite;

        [SerializeField] Teleport teleport;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            teleport.canInteract = false;
            player = Player.Player.main;
            if (Language.LanguageManager.instance.pl)
                letter.GetComponent<Image>().sprite = polishSprite;
            if (Language.LanguageManager.instance.en)
                letter.GetComponent<Image>().sprite = englishSprite;
            letter.SetActive(false);
        }

        void OnMouseDown()
        {
            Open();
            opened = !opened;
            if(!teleport.canInteract)
                teleport.canInteract = true;
        }

        void Open()
        {
            if (!opened)
            {
                //if (!played) { audioSource.Play(); played = true; }
                letter.SetActive(true);
                player.GetComponent<Player.Player>().enabled = false;
                player.GetComponent<Player.PlayerAnimations>().enabled = false;

            }
            if (opened)
            {
                letter.SetActive(false);
                player.GetComponent<Player.Player>().enabled = true;
                player.GetComponent<Player.PlayerAnimations>().enabled = true;
            }
        }
    }
}
