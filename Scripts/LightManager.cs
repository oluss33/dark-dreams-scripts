using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lightning
{
    public class LightManager : MonoBehaviour
    {
        public static LightManager Instance;
        public float lightValue;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            //else
            //{
            //    Debug.Log(gameObject.name);
            //    Destroy(this);
            //    Debug.LogError("More than 1 QuestManager instance!");
            //    return;
            //}
            DontDestroyOnLoad(gameObject);
        }
    }
}
