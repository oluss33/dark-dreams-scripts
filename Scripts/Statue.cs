using System.Collections;
using System.Collections.Generic;
using Scripts.Player.Movement;
using UnityEngine;

namespace Scripts {
    public class Statue : MonoBehaviour
    {
        private void OnMouseDown()
        {
            if(!GameManager.Instance.usedStatue)
            {
                InfoCard.InfoCardManager.instance.Open("Degenerated Deity", "Worship a degenerated deity to save the game,  but do it carefully, because every time you worship them, you become part of this world.", "Spaczone Bostwo", "Oddawaj czesc spaczonemu bostwu, aby zapisac stan gry. Rob to jednak rozwaznie, gdyz za kazdym razem gdy oddajesz mu czesc, stajesz sie czescia tej krainy...", InfoCard.InfoCardManager.instance.statueSprite);
                GameManager.Instance.usedStatue = true;
            }
            GameManager.Instance.Save();
            AnimatePlayer();
        }

        public void AnimatePlayer()
        {
            PlayerController.instance.enabled = false;
            Player.PlayerAnimations.Instance.enabled = false;
            Player.Player.main.GetComponent<Animator>().SetBool("Statue", true);
            StartCoroutine(Anim());
        }

        IEnumerator Anim()
        {
            yield return new WaitForSeconds(5f);
            PlayerController.instance.enabled = true;
            Player.PlayerAnimations.Instance.enabled = true;
            Player.Player.main.GetComponent<Animator>().SetBool("Statue", false);
        }
    }
}