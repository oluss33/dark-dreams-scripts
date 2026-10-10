using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts {
    public class Checkpoint : MonoBehaviour
    {

        private bool used = false;
        void OnTriggerEnter2D(Collider2D col) {
            if(col.CompareTag(Tags.Player) && !used) {
                GameManager.Instance.currentCheckpoint = transform;
                print("used checkpoint on " + transform.position + " pos.");
                used = true;
            }
        }
    }
}
