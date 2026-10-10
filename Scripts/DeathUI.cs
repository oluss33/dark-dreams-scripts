using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts
{
    public class DeathUI : MonoBehaviour
    {
        public static DeathUI instance { get; private set; }

        [SerializeField] private GameObject ui;
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private TMPro.TMP_Text t;

        Player.Player player;

        Player.PlayerHealth health = PlayerHealth.instance;
        /*   public GameObject deathUI;
           public GameObject optionsUI;*/

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            player = Player.Player.main;
            panel.SetActive(false);

            foreach (var button in buttons)
            {
                button.SetActive(false);
            }

            t.gameObject.SetActive(false);
            ui.SetActive(false);
        }

        public void ShowUI()
        {
            ui.SetActive(true);
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }

            panel.SetActive(true);
            t.gameObject.SetActive(true);
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void Respawn()
        {
            RespawnManager.ResetWorld();
            ui.gameObject.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }

        /* public void Options()
         {
             deathUI.SetActive(false);
             optionsUI.SetActive(true);
         }*/

        /*     public void LoadGame()
             {
                 player.transform.position = GameManager.Instance.lastCheckpointPos;
                 health.AddHealth((int)health.maxHp);
                 health.AddHealth((int)health.maxHp);
                 health.AddHealth((int)health.maxHp);

                 PlayerController.instance.enabled = true;
                 PlayerAnimations.Instance.enabled = true;
                 player.GetComponent<Animator>().SetBool("stay", true);
                 player.died = false;
                 player.deathUI.SetActive(false);

                 Saving.SavingSystem.Load();
             }
             */

        public void NewGame()
        {
            SceneManager.LoadScene("Demo");
        }
    }
}