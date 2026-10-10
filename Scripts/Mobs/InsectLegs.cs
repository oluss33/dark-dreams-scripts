using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Mobs
{
    public class InsectLegs : MonoBehaviour
    {
        [SerializeField] private Transform limbSolverTarget;
        [SerializeField] private float moveDistance;

        private LayerMask groundLayer;

        private void Start()
        {
            groundLayer = LayerMasks.instance.groundMask;
        }

        private void Update()
        {
            CheckGround();
            if (Vector2.Distance(limbSolverTarget.position, transform.position) > moveDistance)
            {
                limbSolverTarget.position = transform.position;
            }
        }

        private void CheckGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 5, groundLayer);
            if (hit.collider != null)
            {
                Vector2 point = hit.point;
                point.y += 0.1f;
                transform.position = point;
            }
        }
    }
}