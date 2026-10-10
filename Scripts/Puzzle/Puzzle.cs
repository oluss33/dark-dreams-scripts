using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Puzzle
{
    public class Puzzle : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [Header("UI")] 
        public Image img;

        private LayoutElement lElement;

        Vector3 startPos;



        private void Start()
        {
            lElement = GetComponent<LayoutElement>();
        }

        public void SetupPuzzle(Sprite icon)
        {
            this.img.sprite = icon;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            startPos = transform.position;
            MovePuzzle();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            return;
        }

        void MovePuzzle()
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
           // lElement.ignoreLayout = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Puszczam puzzel!");
            if (eventData.pointerEnter != null)
            {
                if (eventData.pointerEnter.gameObject.tag == "Puzzle")
                {
                    var thisIndex = transform.GetSiblingIndex();
                    var newIndex = eventData.pointerEnter.transform.GetSiblingIndex();

                    transform.SetSiblingIndex(newIndex);
                    eventData.pointerEnter.transform.SetSiblingIndex(thisIndex);
                }
                else
                {
                    Debug.Log("Ustawiam pozycje");
                    transform.position = startPos; // ten fragment nie dziala 
                }
            } else
            {
                Debug.Log("Ustawiam pozycje");
                transform.position = startPos; // ten framgent nie dziala
            }
            GetComponent<Image>().raycastTarget = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            GetComponent<Image>().raycastTarget = false;
            gameObject.transform.position = Input.mousePosition;
        }
    }
}