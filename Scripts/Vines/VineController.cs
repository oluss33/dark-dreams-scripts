using System;
using UnityEngine;

namespace Vines
{
    public class VineController : MonoBehaviour
    {
        [SerializeField] private LayerMask mask;

        private Vine activeVine;

        public bool isHanging;
        
        public static VineController instance { get; private set; }
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        void SetPos()
        {
            isHanging = true;
            transform.position = activeVine.transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (activeVine != null)
                {
                    var i = Array.IndexOf(activeVine.area.vines, activeVine);
                    activeVine = activeVine.area.vines[i + 1];
                    SetPos();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                var a = Physics2D.OverlapCircle(transform.position, 5, mask);

                if (a != null)
                {
                    print(a.name);

                    Vine vine = a.GetComponent<Vine>();
                    activeVine = vine;
                    SetPos();
                }
            }
        }
    }
}