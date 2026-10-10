using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Tooltip
{
    public class TooltipWarning : MonoBehaviour
    {
        private static TooltipWarning instance;

        Camera _camera;
        [SerializeField] RectTransform canvasRectTransform;

        Text tooltipText;
        Image bgImage;
        RectTransform bgRectTransform;
        Func<string> getTooltipString;

        float showTimer;
        float flashTimer;
        int flashState;

        private void Awake()
        {
            instance = this;
            bgRectTransform = transform.Find("Background").GetComponent<RectTransform>();

            tooltipText = transform.Find("Text").GetComponent<Text>();

            bgImage = transform.Find("Background").GetComponent<Image>();


            HideTooltip();
        }

        private void Update()
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, _camera, out localPoint);
            transform.localPosition = localPoint;

            SetText(getTooltipString());

            Vector2 anchoredPos = transform.GetComponent<RectTransform>().anchoredPosition;
            if(anchoredPos.x + bgRectTransform.rect.width > canvasRectTransform.rect.width)
            {
                anchoredPos.x = canvasRectTransform.rect.width - bgRectTransform.rect.width;
            }
            if(anchoredPos.y + bgRectTransform.rect.height > canvasRectTransform.rect.height)
            {
                anchoredPos.y = canvasRectTransform.rect.height - bgRectTransform.rect.height;
            }
            transform.GetComponent<RectTransform>().anchoredPosition = anchoredPos;

            flashTimer += Time.deltaTime;
            float flashTimerMax = 0.05f;
            if(flashTimer > flashTimerMax)
            {
                flashState++;
                switch(flashState)
                {
                    case 1:
                    case 3:
                    case 5:
                        tooltipText.color = new Color(1, 1, 1, 1);
                        bgImage.color = new Color(178 / 255f, 0 / 255f, 0 / 255f, 1);
                        break;
                    case 2:
                    case 4:
                        tooltipText.color = new Color(178 / 255f, 0 / 255f, 0 / 255f, 1);
                        bgImage.color = new Color(1, 1, 1, 1);
                        break;
                }
            }    

            showTimer -= Time.deltaTime;
            if(showTimer <= 0f)
            {
                HideTooltip();
            }
        }

        private void ShowTooltip(string s, float showTimerMax = 2f)
        {
            ShowTooltip(() => s, showTimerMax);
        }

        private void ShowTooltip(Func<string> getTooltipString, float showTimerMax = 2f)
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
            this.getTooltipString = getTooltipString;
            showTimer = showTimerMax;
            flashTimer = 0;
            flashState = 0;
        }

        private void SetText(string s)
        {
            tooltipText.text = s;
            float textPaddingSize = 4f;
            Vector2 bgSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
            bgRectTransform.sizeDelta = bgSize;
            tooltipText.GetComponent<RectTransform>().sizeDelta = bgSize;
        }

        private void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        public static void ShowTooltip_Static(string s)
        {
            instance.ShowTooltip(s);
        }

        public static void HideTooltip_Static()
        {
            instance.HideTooltip();
        }
    }
}