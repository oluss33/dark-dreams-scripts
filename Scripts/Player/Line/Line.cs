using System;
using Scripts.Player.Movement;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Scripts.Player.Line
{
    public class Line : MonoBehaviour
    {
        public GameObject player;
        private Animator playerAnim;
        public Transform attachment;
        public Line nextLine;
        public Transform landPosition;

        private bool isHanging;

        public UnityEvent onSucceed;

        public UnityEvent onFailed;

        public bool QTE_Shown = false;
        
        private void Start()
        {
            playerAnim = player.GetComponent<Animator>();
        }

        private void Update()
        {
            
            if (isHanging)
            {
                playerAnim.SetBool("isHanging", true);
                if (!QTE_Shown)
                {
                    QuickTimeEvent.instance.GenerateQTE(5, onSucceed, onFailed);
                    QTE_Shown = true;
                }
                
            }
        }

        public void Jump()
        {
            playerAnim.SetTrigger("LineJump");
            if (nextLine != null)
            {
                isHanging = false;
                nextLine.QTE_Shown = false;
                nextLine.Attach();
            }
            else
            {
                Land();
            }
        }


        public void Attach()
        {
            player.LeanMove(attachment.position, 5).setEaseOutBack();
            PlayerController.instance.DisableControls();
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            isHanging = true;
            QTE_Shown = false;
        }

        public void Land()
        {
            playerAnim.SetBool("isHanging", false);
            player.LeanMove(landPosition.position, 5).setEaseOutBack();
            PlayerController.instance.EnableControls();
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isHanging = false;
        }

        private void OnMouseDown()
        {
            Attach();
        }

        public void OnFailed()
        {
            Destroy(player);
        }
    }
}