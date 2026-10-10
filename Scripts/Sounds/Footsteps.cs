using System.Collections;
using System.Collections.Generic;
using Scripts.Player.Movement;
using UnityEngine;

namespace Scripts.Sounds
{
    public class Footsteps : MonoBehaviour
    {
        public AudioClip grassClip;
        public AudioClip stoneClip;
        public AudioClip woodClip;
        Player.Player player;

        Rigidbody2D rb;

        private void Start()
        {
            player = Player.Player.main;
            rb = player.GetComponent<Rigidbody2D>();
        }

        public void Step()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, ~LayerMasks.instance.playerMask);
            if (player.source.isPlaying) return;
            if(player.GetComponent<Rigidbody2D>().linearVelocity.x != 0 && PlayerController.instance.isGrounded)
            {
                if (hit.transform.tag == "Ground")
                {
                    player.source.PlayOneShot(grassClip);
                }
                if(hit.transform.tag == "Stone")
                {
                    player.source.PlayOneShot(stoneClip);
                }
                if(hit.transform.tag == "Wood")
                {
                    player.source.PlayOneShot(woodClip);
                }
            }
            else
            {
                player.source.Stop();
            }
        }
    }
}
