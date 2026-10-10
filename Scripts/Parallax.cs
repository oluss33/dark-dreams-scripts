using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Parallax : MonoBehaviour
    {
        float length;
        float startPos;

        public float increment;

        private void Start()
        {
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            float temp = (Player.Player.main.cameraTR.position.x * (1 - increment));
            float dist = (Player.Player.main.cameraTR.position.x * increment);

            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            if (temp > startPos + length) startPos += length;
            else if (temp < startPos - length) startPos -= length;
        }
    }
}