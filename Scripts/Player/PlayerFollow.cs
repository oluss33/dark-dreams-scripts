using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    namespace Scripts.Player {
    public class PlayerFollow : MonoBehaviour
    {
        public float speed;
        private Transform target;

        void Start()
        {
            target = Player.main.GetComponent<Transform>();
        }


        void Update()
        {
            if(Vector2.Distance(transform.position, target.position) > 3)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
