using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class PushController : MonoBehaviour
    {
        public float dist = 5f;
        public LayerMask mask;

        GameObject go;

        private void Update()
        {
            Physics2D.queriesStartInColliders = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, dist, mask);

            if (hit.collider != null && Input.GetKey(KeyCode.E))
            {
                go = hit.collider.gameObject;
                go.GetComponent<FixedJoint2D>().enabled = true;
                go.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
                go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                Player.main.GetComponent<Animator>().SetBool("isHolding", true);
                Player.main.canRotate = false;
                Player.main.canRun = false;
            }
            else if (Input.GetKeyUp(KeyCode.E) && go !=  null)
            {
                go.GetComponent<FixedJoint2D>().enabled = false;
                Player.main.GetComponent<Animator>().SetBool("isHolding", false);
                Player.main.canRotate = true;
                Player.main.canRun = true;
                go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * dist);
        }
    }
}
