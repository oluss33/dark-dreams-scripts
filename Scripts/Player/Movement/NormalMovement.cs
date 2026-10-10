using System;
using Scripts.Sounds;
using UnityEngine;
using UnityEngine.InputSystem;
using Vines;

namespace Scripts.Player.Movement
{
    public class NormalMovement : Movement
    {
        private Player player;

        private float jumpForce;

        private Transform groundCheck;

        private Vector2 v = Vector2.zero;

        private bool doubleJump = false;
        
        public NormalMovement(float speed, float jumpForce)
        {
            this.speed = speed;
            this.jumpForce = jumpForce;
        }

        public override void StartMovement(PlayerController controller)
        {
            this.controller = controller;

            this.controls = controller.controls;
            this.rb = controller.rb;
            this.groundCheck = controller.groundCheck;

        }

        public override void Update()
        {

            moveDirection = controls.Player.Movement.ReadValue<Vector2>();

            moveDirection.Normalize();
            
            if (controller.isGrounded)
            {
                if (!controls.Player.Jump.WasPressedThisFrame() && doubleJump && controller.isGrounded) doubleJump = false;
                
                if (controls.Player.Jump.WasPressedThisFrame())
                {
                    Debug.Log("Jump");
                    shouldJump = true;
                }
            }
            else
            {
                // check is the ability unlocked
                if (controls.Player.Jump.WasPressedThisFrame() && !doubleJump)
                {
                    shouldJump = true;
                    Debug.Log("doubleJump");
                    Player.main.anim.SetTrigger("DoubleJump");
                    doubleJump = true;
                }
            }

            if (!Tracker.Instance.isTracking)
            {
                if (moveDirection.x > 0.01f)
                {
                    rb.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (moveDirection.x < -0.01f)
                {
                    rb.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }

        public override void FixedUpdate()
        {
            if (VineController.instance.isHanging) return;
            //if (moveDirection.x == 0) rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            v = rb.linearVelocity;
            
            Vector2 gravity = Vector2.zero;

            gravity += -1 * (Vector2)rb.transform.up * controller.gravity;

            if (shouldJump)
            {
                if (controller.performsDodge) return;
                //v += (Vector2) rb.transform.up * jumpForce;
                rb.AddForce(rb.transform.up * jumpForce, ForceMode2D.Impulse);
                shouldJump = false;
                controller.isGrounded = false;
                PlayerSounds.instance.PlayJumpSound();
                return;
            }
            
            if (controller.isGrounded)
            {
                 // for same speed with gamepads ;)
                
                var move = (moveDirection.x * rb.transform.right) * speed;
                v.x = move.x;
                
                Debug.DrawRay(rb.transform.position, v);

                v.x *= speedModifier; // e.g. for Dodge
                
                
                
            }
            else
            {
                if (useGravity)
                {
                    v += gravity * Time.fixedDeltaTime;
                }
            }

            
            rb.linearVelocity = new Vector2(v.x, v.y);
        }
    }
}