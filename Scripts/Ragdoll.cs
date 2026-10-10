using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Collider2D[] myColliders;
    [SerializeField] private Animator anim;
    [SerializeField] private List<Collider2D> colliders;
    [SerializeField] private List<HingeJoint2D> joints;
    [SerializeField] private List<Rigidbody2D> rbs;
    [SerializeField] private List<LimbSolver2D> solvers;

    private Rigidbody2D rb;
    
    public void ToggleRagdoll(bool isOn)
    {
        rb.linearVelocity = Vector2.zero;
        anim.enabled = !isOn;
        if (isOn) rb.bodyType = RigidbodyType2D.Static;
        else rb.bodyType = RigidbodyType2D.Dynamic;
        foreach (var col in colliders)
        {
            col.enabled = isOn;
        }

        foreach (var joint in joints)
        {
            joint.enabled = isOn;
        }

        foreach (var rb in rbs)
        {
            rb.simulated = isOn;
        }

        foreach (var solver in solvers)
        {
            solver.weight = isOn ? 0 : 1;
        }

        foreach (var col in myColliders)
        {
            col.enabled = !isOn;
        }
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ToggleRagdoll(false);
        foreach (Collider2D col in colliders)
        {
            foreach (Collider2D myCol in myColliders)
            {
                Physics2D.IgnoreCollision(col, myCol);
            }
        }
    }

   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleRagdoll(true);
        }
    }*/
}
