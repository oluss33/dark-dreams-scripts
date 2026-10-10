using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.NPC {
    public class Boy : MonoBehaviour
    {
        public bool gaveSweet = false;

        public void GiveSweet()
        {
            FindObjectOfType<Boy>().gaveSweet = true;
        }
    }
}
