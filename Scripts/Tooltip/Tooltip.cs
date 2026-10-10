using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Scripts.Tooltip
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerTxt;
        [SerializeField] private TextMeshProUGUI contentTxt;
        [SerializeField] private LayoutElement layoutElement;

        [SerializeField] private int characterLimit;

        [SerializeField] private RectTransform rectTransform;

        [SerializeField] private Vector2 offset;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }


        public void SetText(string content, string header = "")
        {
            if (string.IsNullOrEmpty(header))
            {
                headerTxt.gameObject.SetActive(false);
            }
            else
            {
                headerTxt.gameObject.SetActive(true);
                headerTxt.text = header;
            }

            contentTxt.text = content;
            
            int headerLength = headerTxt.text.Length;
            int contentLength = contentTxt.text.Length;
            
            layoutElement.enabled = (headerLength > characterLimit || contentLength > characterLimit) ? true : false;
        }

        private void Update()
        {
            Vector2 pos = Input.mousePosition;

            float pivotX = pos.x / Screen.width;
            float pivotY = pos.y / Screen.height;
                
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            transform.position = pos + offset;
        }
    }
}