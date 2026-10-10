using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Menu
{
    public class MenuButtonController : MonoBehaviour
    {
        public int currentIndex;
        public int maxIndex;
        bool keyDown;

        public static MenuButtonController Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (!keyDown)
                    {
                        if (currentIndex < maxIndex)
                            currentIndex++;
                        else
                            currentIndex = 0;
                        keyDown = true;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (!keyDown)
                    {
                        if (currentIndex > 0)
                            currentIndex--;
                        else
                            currentIndex = maxIndex;
                        keyDown = true;
                    }
                }
            }
            else
            {
                keyDown = false;
            }
        }
    }
}
