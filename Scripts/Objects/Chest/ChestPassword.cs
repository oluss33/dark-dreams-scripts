using Scripts.Tooltip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Chest
{
    public class ChestPassword : MonoBehaviour, Openable
    {

        public static ChestPassword instance { get; private set; }

        public InputField inp;
        string pass;

        Chest thisChest;

        public bool IsOpened()
        {
            return opened;
        }

        public bool opened = false;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        
        public void SetPassword(string password, Chest chest)
        {
            pass = password;
            thisChest = chest;
        }

        public void CheckPassword()
        {
            if(inp.text.ToLower() == pass.ToLower())
            {
                thisChest.OpenChest();
                gameObject.SetActive(false);
                opened = false;
            }
            else
            {
                inp.text = "";
                TooltipWarning.ShowTooltip_Static("Wrong password!");
            }
        }

        public void Back()
        {
            gameObject.SetActive(false);
            opened = false;
        }
    }
}
