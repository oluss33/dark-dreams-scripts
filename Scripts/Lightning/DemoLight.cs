using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Lightning
{
    public class DemoLight : MonoBehaviour
    {

        public static DemoLight instance { get; private set; }
        public UnityEngine.Rendering.Universal.Light2D light2d;

        private void Awake()
        {
            instance = this;
            light2d = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }

        void Start()
        {
            light2d.intensity = LightManager.Instance.lightValue;
        }

    }
}
