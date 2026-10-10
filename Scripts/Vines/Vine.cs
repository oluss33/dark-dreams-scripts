using UnityEngine;

namespace Vines
{
    public class Vine : MonoBehaviour
    {
        public VinesArea area;

        private void Start()
        {
            area = transform.parent.GetComponent<VinesArea>();
        }
    }
}