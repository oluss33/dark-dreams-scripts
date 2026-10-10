using System;
using Discord;
using UnityEngine;

namespace Scripts.Tooltip
{
    public class TooltipSystem : MonoBehaviour
    {
        private static TooltipSystem instance;

        public Tooltip tooltip;
        
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            print(instance);
            Hide();
        }

        public static void Show(string content, string header = "")
        {
            instance.tooltip.SetText(content, header);
            instance.tooltip.gameObject.SetActive(true);
        }
        
        public static void Hide()
        {
            instance.tooltip.gameObject.SetActive(false);
        }
    }
}