using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class LocationsUI : MonoBehaviour
    {

        public static LocationsUI instance {get; private set;}

        [SerializeField] GameObject[] locations;
        public int index = 0;
        public int canUnlockToIndex = 0;

        public LocationElement mooshland;

        void Awake()
        {
            instance = this;           
        }

        public void Start()
        {
            for (int i = 0; i < locations.Length; i++)
            {
                locations[i].SetActive(false);
            }
            if(locations.Length > 0)
                locations[0].SetActive(true);

            mooshland = locations[0].GetComponent<LocationElement>();
        }


        public void Next()
        {
            if (index == canUnlockToIndex) return;
            if (index < canUnlockToIndex)
            {
                index++;
                for (int i = 0; i < locations.Length; i++)
                {
                    locations[i].SetActive(false);
                }
                locations[index].SetActive(true);
            }
        }
        public void Previous()
        {
            if (index == canUnlockToIndex) return;
            if (index > 0)
            {
                index--;
                for (int i = 0; i < locations.Length; i++)
                {
                    locations[i].SetActive(false);
                }
                locations[index].SetActive(true);

            }
        }
    }
}