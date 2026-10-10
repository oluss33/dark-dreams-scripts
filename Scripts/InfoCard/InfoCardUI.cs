using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.InfoCard
{
    public class InfoCardUI : MonoBehaviour
    {
        public Image img;

        public Text titleTxt;
        public Text descTxt;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Show(string en_titleText, string en_descriptionText, string pl_titleText, string pl_descriptionText, Sprite s)
        {
            img.sprite = s;
            if (Language.LanguageManager.instance.en)
            {
                titleTxt.text = en_titleText;
                descTxt.text = en_descriptionText;
            }
            if(Language.LanguageManager.instance.pl)
            {
                titleTxt.text = pl_titleText;
                descTxt.text = pl_descriptionText;
            }

            if(s == InfoCardManager.instance.statueSprite)
            {
                img.rectTransform.sizeDelta = new Vector2(81, 181);
            }
        }
    }
}