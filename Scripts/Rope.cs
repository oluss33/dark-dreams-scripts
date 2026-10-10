using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private GameObject[] segmentsPrefabs;

        [SerializeField] private int numLinks;

        [SerializeField] private float rotationSpeed, maxRotation, rotationMultiplier;

        public List<GameObject> links;

        private Vector2 previousPos;

        private float[] angles;

        public Vector3 scale;
        private void Start()
        {
            links = new List<GameObject>();
            angles = new float[numLinks];
            for(int i = 0; i < numLinks; i++)
            {
                var determined_pos = new Vector2(0, 0);
                var parent = transform;
                var determined_scale = segmentsPrefabs[i].transform.localScale;
                if (i > 0)
                {
                    
                    var sr = segmentsPrefabs[i - 1].GetComponent<SpriteRenderer>();

                    var y = sr.sprite.rect.height / sr.sprite.pixelsPerUnit;
                    
                    determined_scale = Vector3.one;
                    determined_pos = new Vector2(0,
                        previousPos.y - y);
                    parent = links[i - 1].transform;
                }
                GameObject link = Instantiate(segmentsPrefabs[UnityEngine.Random.Range(0, segmentsPrefabs.Length)], determined_pos, Quaternion.identity, parent);
                link.transform.localScale = determined_scale;
                links.Add(link); 
                previousPos = determined_pos;
            }
        }

        void Update()
        {
            for (int i = 0; i < links.Count; i++)
            {
                if (i == 0)
                {
                    var angle0 = Mathf.Sin(Time.time * rotationSpeed) * maxRotation;
                    angles[0] = angle0;
                }
                else
                {
                    var angle = angles[i - 1] * (1 + rotationMultiplier);
                    angles[i] = angle;

                }
                links[i].transform.localRotation  = Quaternion.Euler(0, 0, angles[i]);
            }
        }
    }
}