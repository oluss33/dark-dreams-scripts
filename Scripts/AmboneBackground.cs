using System;
using UnityEngine;

namespace Scripts
{
    public class AmboneBackground : MonoBehaviour
    {
        [SerializeField] private GameObject[] elements;

        public float speed;
        private float leftLimit;
        private float rightLimit;
        private float center;
        public bool turnLeft = true;

        private void Start()
        {
            leftLimit = elements[0].transform.position.x;
            rightLimit = elements[^1].transform.position.x;
            center = elements[1].transform.position.x;
        }

        void Update()
        {
            if (turnLeft)
            {
                foreach (GameObject g in elements)
                {
                    g.transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
                }
            }
            else
            {
                foreach (GameObject g in elements)
                {
                    g.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                }
            }

            if (elements[0].transform.position.x >= rightLimit && !turnLeft)
            {
                turnLeft = true;
            }

            if (elements[^1].transform.position.x <= leftLimit && turnLeft)
            {
                turnLeft = false;
            }
            
            /*
            if (elements[0].transform.position.x < center && turnLeft)
            {
                foreach (GameObject g in elements)
                {
                    g.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                }
            }
            else
            {
                foreach (GameObject g in elements)
                {
                    g.transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
                }

                if (elements[0].transform.position.x == center)
                {
                    turnLeft = true;
                }
            }
            */
        }
    }
}