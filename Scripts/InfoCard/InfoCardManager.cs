using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InfoCard
{
    public class InfoCardManager : MonoBehaviour
    {
        public static InfoCardManager instance { get; private set; }
        public InfoCardUI UI;

        [SerializeField] private float maxTime;
        private float time;
        
        [Header("Sprites")]
        public Sprite statueSprite;

        private void Awake()
        {
            instance = this;
        }


        private void Update()
        {
            if (time > 0) time += 1 * Time.deltaTime;
            if(time > maxTime) Hide();
        }

        public void Open(string engTitle, string engDesc, string plTitle, string plDesc, Sprite s)
        {
            time = 0;
            UI.gameObject.SetActive(true);
            UI.Show(engTitle, engDesc, plTitle, plDesc, s);
            
            
        }

        public void Hide()
        {
            UI.gameObject.SetActive(false);
        }
    }
}
