using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player.Movement
{
    public abstract class Movement
    {
        public float speed = 0;
        public float speedModifier = 1;
        public Vector2 moveDirection;

        protected PlayerController controller;
        protected PlayerControls controls;
        protected Rigidbody2D rb;
        public bool shouldJump = false;
        public bool useGravity = true;

        public virtual void StartMovement(PlayerController controller) { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

    }
}

/*
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Scripts.Player.Line;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Scripts.Player
{
    public abstract class Movement
    {
        public Rigidbody2D rb;
        public float speed;
        public Vector2 moveDir = Vector2.zero;
        public bool isFacingRight = true;

        public virtual void Start(Player p)
        {
        }

        // errors = make update abstract
        public virtual void Update()
        {
        }

        public abstract void FixedUpdate();

        public virtual void Dodge()
        {
        }
    }

    public class NormalMovement : Movement
    {
        public bool isTouchingRoof;
        float lastDirection;
        float horizontal;

        Vector2 inputVector;
        Player player;

        Vector3 scale;

        bool shouldJump = false;

        public NormalMovement(float speed)
        {
            this.speed = speed;
        }


        public override void Start(Player p)
        {
            player = Player.main;
            rb = player.GetComponent<Rigidbody2D>();

            scale = player.transform.localScale;
        }

        public override void Update()
        {
            player.isGrounded =
                Physics2D.OverlapCircle(player.groundCheck.position, .4f, LayerMasks.instance.groundMask);
        }

        public override void FixedUpdate()
        {
            inputVector = Player.main.playerInputActions.Player.Movement.ReadValue<Vector2>();
            if (inputVector.x != 0 && player.isGrounded || inputVector.x != 0 && PlayerController.instance.isHoldingLine)
            {
                //rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
                lastDirection = inputVector.x;

                if (PlayerController.instance.isHoldingLine)
                {
                    rb.velocity = new Vector2(inputVector.x * speed * LineController.instance.Line.speed, rb.velocity.y);
                    LineController.instance.GetRigidbody().velocity = new Vector2(rb.velocity.x * LineController.instance.Line.speed,
                        LineController.instance.GetRigidbody().velocity.y); 
                }
                else rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
            }

            if (inputVector.x == 0) rb.velocity = Vector2.zero;

            if (player.canRotate)
            {
                if (inputVector.x > 0.01f)
                {
                    isFacingRight = true;
                    player.gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                }

                if (inputVector.x < -0.01f)
                {
                    isFacingRight = false;
                    player.gameObject.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                }
            }

        }
    }

    public class SwimMovement : Movement
    {
        float horizontal;
        bool isFacingRight;

        Vector2 inputVector;
        Player player;

        float force;

        Vector3 scale;

        public SwimMovement(float speed)
        {
            this.speed = speed;
        }


        public override void Start(Player p)
        {
            player = Player.main;
            rb = player.GetComponent<Rigidbody2D>();

            scale = player.transform.localScale;
            force = 5;
        }

        public void Swim()
        {
            rb.AddForce(new Vector2(0, force), ForceMode2D.Force);
        }


        public override void Update()
        {
            if (player.playerInputActions.Player.Swimming.triggered)
            {
                Swim();
            }
        }

        public override void FixedUpdate()
        {

            inputVector = Player.main.playerInputActions.Player.Movement.ReadValue<Vector2>();
            rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);

            if (inputVector.x > 0.01f) player.gameObject.transform.localScale = new Vector3(Mathf.Sign(scale.x), scale.y, scale.z);
            if (inputVector.x < -0.01f)
                player.gameObject.transform.localScale = new Vector3(Mathf.Sign(-scale.x), scale.y, scale.z);
        }


    }

    public class LineMovement : Movement
    {
        float horizontal;
        bool isFacingRight;

        Vector2 inputVector;
        Player player;


        Vector3 scale;

        public float jumpForce;

        public LineMovement(float speed)
        {
            this.speed = speed;
        }

        public override void Start(Player p)
        {
            player = Player.main;
            rb = player.GetComponent<Rigidbody2D>();

            scale = player.transform.localScale;

            jumpForce = 15;
        }

        public override void FixedUpdate()
        {
        }
    }
}
*/