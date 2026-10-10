using System;
using UnityEngine;
using TMPro;

namespace Scripts
{
    public class ChatBubble : MonoBehaviour
    {
        private SpriteRenderer backgroundSR;
        private SpriteRenderer iconSR;
        private TextMeshPro textMP;

        public static void Create(Transform parent, Vector3 localPosition, string text, float time)
        {
            Transform t = Instantiate(GameAssets.instance.chatBubble, parent);
            t.gameObject.SetActive(true);
            t.localPosition = localPosition;
            
            t.GetComponent<ChatBubble>().Setup(text);

            Destroy(t.gameObject, time);
        }

        private void Awake()
        {
            backgroundSR = transform.Find("Background").GetComponent<SpriteRenderer>();
            iconSR = transform.Find("Icon").GetComponent<SpriteRenderer>();
            textMP = transform.Find("Text").GetComponent<TextMeshPro>();
        }

        private void Start()
        {
        }

        private void Setup(string text)
        {
            textMP.SetText(text);

            float textScale = 0.5f;
            textMP.transform.localScale = Vector3.one * textScale;

            textMP.ForceMeshUpdate();
            Vector2 textSize = textMP.GetRenderedValues(false) * textScale;

            Vector2 padding = new Vector2(1 ,1); 
            backgroundSR.size = textSize + padding;
        }
    }
}
