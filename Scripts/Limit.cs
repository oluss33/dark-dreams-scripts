using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts {
    public class Limit : MonoBehaviour
    {
        public Vector3 offset;

        void Update()
        {
            if (Player.Player.main.GetComponent<Rigidbody2D>().linearVelocity.magnitude > 0)
            {
                transform.position = Player.Player.main.transform.position + offset;
            }
        }
    }
}
