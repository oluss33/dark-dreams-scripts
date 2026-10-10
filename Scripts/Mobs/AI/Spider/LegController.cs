using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Mobs.AI.Spider
{
    public class LegController : MonoBehaviour
    {
        [SerializeField] private Transform limbTarget;
        [SerializeField] private float maxDistance;

        private float requiredTime = 3;
        private float time = 0;


        private bool shouldLerp = false;
        
        private void Update()
        {

            float dist = Vector2.Distance(transform.position, limbTarget.position);
            if (dist > maxDistance)
            {
                shouldLerp = true;
            }

            if (shouldLerp)
            {
                time += Time.deltaTime;
                float duration = time / requiredTime;
                Vector2 pos = Vector2.Lerp(limbTarget.position, transform.position, duration);
                limbTarget.position = pos;

                if (transform.position == limbTarget.position) { Debug.Log("Wylaczam lerp"); shouldLerp = false;
                    time = 0;
                }
            }

            CheckGround();
        }

        private void CheckGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, LayerMasks.instance.groundMask);

            if (hit.point != null)
            {
                hit.point = new Vector2(hit.point.x, hit.point.y + 0.5f);
                transform.position = hit.point;
            }
        }
    }
}
