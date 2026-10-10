using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player.Movement;
using Scripts.Combat.Player;

namespace Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        Animator anim;
        Player player;
        PlayerController playerController;
        Rigidbody2D rb;
        AudioSource source;
        private PlayerControls controls;

        public static PlayerAnimations Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        void Start()
        {
            player = Player.main;
            anim = GetComponent<Animator>();
            source = GetComponent<AudioSource>();
            playerController = GetComponent<PlayerController>();
            rb = GetComponent<Rigidbody2D>();
                
            controls = playerController.controls;
            //controls.Player.Movement.performed += ctx => Move();
            controls.Player.Movement.canceled += ctx => anim.SetFloat("moveSpeed", 0);
                //  actions.Attack.Attack.performed += ctx => anim.SetTrigger("Attack");
            
        }

        private void Update()
        {

            if (/*controls.Player.Movement.IsPressed()*/ Math.Abs(controls.Player.Movement.ReadValue<Vector2>().x) > 0)
            {
                Move();
            }
            
          //  if(playerController.isJumping())  anim.SetBool("isJumping", true);
          //  else anim.SetBool("isJumping", false);

           // if(playerController.isCrouching) anim.SetBool("isCrouching", true);
           // else anim.SetBool("isCrouching", false);

            if(playerController.IsRunning()) anim.SetBool("isRunning", true);
            else anim.SetBool("isRunning", false);
        }

        void Move()
        {
            if (playerController.isGrounded)
            {
                anim.SetFloat("moveSpeed", 1);
            }
        }
        
        
    }
}
