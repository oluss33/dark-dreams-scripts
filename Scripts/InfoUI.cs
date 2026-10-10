using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class InfoUI : MonoBehaviour
    {
        public GameObject UI;
        [SerializeField] Text nameText, descText, costText;


        public static InfoUI instance { get; private set; }

        private void Awake()
        {
            instance = this;
            UI.SetActive(false);
        }

        public void Setup(string skillName, string skillDescription, int skillCost)
        {
            nameText.text = skillName;
            descText.text = skillDescription;
            costText.text = "Cost: " + skillCost.ToString();
        }
    }
}