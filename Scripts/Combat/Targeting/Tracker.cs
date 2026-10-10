using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Runtime.InteropServices.ComTypes;
using Scripts.Player.Movement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

namespace Scripts.Player
{
    public class Tracker : MonoBehaviour
    { 
        public static Tracker Instance { get; private set; } 
        public float maxTargetingDistance;

        public GameObject target { get; private set; }
        public bool isTracking { get; private set; } = false;
        private Player player;

        private GameObject indicator;
        
        
        void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            player = Player.main;
            PlayerController.instance.controls.Attack.EnableTargetting.performed += ctx => EnableTargeting();
            PlayerController.instance.controls.Attack.targetRight.performed += ctx => FindTarget(Vector2.right, target, false);
            PlayerController.instance.controls.Attack.targetLeft.performed += ctx => FindTarget(Vector2.left, target, false);;
        }

        private void Flip()
        {
            player.transform.localScale *= new Vector2(-1, 1);
            print("im changing player rotation");
        }
        
        private void Update()
        {
            if (target != null)
            {
                if (target.transform.position.x > player.transform.position.x)
                {
                    if (player.transform.localScale.x < 0)
                    {
                        Flip();
                    }
                    else print("player rotation is fine! :)");
                }

                if (target.transform.position.x < player.transform.position.x)
                {
                    if (player.transform.localScale.x > 0)
                    {
                        Flip();
                        print("im changing player rotation");
                    }
                    else print("player rotation is fine! :)");
                }
            }
            
            if(isTracking && target == null)
            {
                FindTarget(new Vector2(transform.localScale.x, 0), gameObject, false);
            }
        }

        void EnableTargeting()
        {
            if (!isTracking)
            {
                if(FindTarget(new Vector2(transform.localScale.x, 0), gameObject, true))
                {
                    isTracking = true;
                }
            }
            else
            {
                isTracking = false;
                target = null;
                indicator.SetActive(false);
                indicator = null;
            }
        }

        private bool FindTarget( Vector2 direction, GameObject origin, bool isCalledFromEnableTargeting)
        {
            if(!isTracking && !isCalledFromEnableTargeting)
            {
                return false;
            }
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin.transform.position, direction, maxTargetingDistance, LayerMasks.instance.enemyMask);
            if (hits.Length == 0) return false;
            if (hits[0].collider != null)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    GameObject hitObject = hit.collider.transform.root.gameObject;
                    if (GameObject.ReferenceEquals(origin.transform.root, hitObject.transform.root))
                    {
                        continue;
                    }
                    else
                    {
                        target = hitObject;
                        if (indicator != null)
                        {
                            indicator.SetActive(false);
                        }
                        indicator = target.transform.Find("targetIndicator").gameObject;
                        if (indicator != null)
                        {
                            indicator.SetActive(true);
                        }
                        return true;
                    }
                }
            }
            NoTargetsErr();
            return false;
        }

        public void NoTargetsErr()
        {
            Debug.LogError("No targets found!");
        }
    }
}
