using System;
using UnityEngine;

namespace Scripts.Player.Movement
{
    public class LadderMovement : MonoBehaviour
    {
        [SerializeField] private float verticalSpeed;
        [SerializeField] private float horizontalSpeed;
        [SerializeField] private AudioClip ladderSounds;
        private bool isClimbing;
        private bool isLadder;

        private float vertical;
        private float horizontal;

        private PlayerController _controller;
        private PlayerControls _controls;
        private Rigidbody2D rb;
        private Animator anim;
        private AudioSource source;

        public bool canInteract = false;
        
        private void Start()
        {
            _controller = PlayerController.instance;
            _controls = _controller.controls;
            rb = _controller.rb;
            anim = Player.main.anim;
            source = Player.main.source;

        }

        private void Update()
        {
            vertical = _controls.Player.Movement.ReadValue<Vector2>().y;
            horizontal = _controls.Player.Movement.ReadValue<Vector2>().x;

            if (canInteract)
            {
                if (_controls.Player.Interact.WasPressedThisFrame())
                {
                    isLadder = true;
                    anim.SetBool("isLadder", true);
                }
            }
            
            if (isLadder && Math.Abs(vertical) > 0f)
            {
                isClimbing = true;
                anim.SetFloat("ladderSpeed", 1);
                if(!source.isPlaying)
                    source.PlayOneShot(ladderSounds);
            }
            else
            {
                anim.SetFloat("ladderSpeed", 0);
            }
        }

        private void FixedUpdate()
        {
            if (isClimbing)
            {
                _controller.GetMovement().useGravity = false;
                _controller.rb.gravityScale = 0f;
                _controller.rb.linearVelocity = new Vector2(horizontal * horizontalSpeed, vertical * verticalSpeed);
            }
            else
            {
                _controller.rb.gravityScale = 1f;
                _controller.GetMovement().useGravity = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ladder"))
            {
                //isLadder = true;
                canInteract = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Ladder"))
            {
                isLadder = false;
                isClimbing = false;
                anim.SetBool("isLadder", false);
                anim.SetFloat("ladderSpeed", 0);
                canInteract = false;
            }
        }
    }
}