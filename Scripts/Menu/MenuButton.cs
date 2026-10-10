using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scripts.Menu
{
    public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        public int index;
        private Animator anim;
        private AudioSource source;
        [SerializeField] public GameObject elements;

        private void Update()
        {
            /*if(MenuButtonController.Instance.currentIndex == index)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
                Select();
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                Deselect();
            }*/
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
            source = GetComponent<AudioSource>();
            elements.SetActive(false);
            GetComponent<Button>().onClick.AddListener(DisableElements);
        }

        void DisableElements()
        {
            Deselect();
            elements.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           // Select();
            elements.SetActive(true);
            if (source != null)
            {
                source.PlayOneShot(source.clip);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
          //  Deselect();
            elements.SetActive(false);
        }

        public void Select()
        {
            anim.SetBool("Selected", true);
        }

        public void Deselect()
        {
            anim.SetBool("Selected", false);
        }

        public void OnSelect(BaseEventData eventData)
        {
           elements.SetActive(true);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            elements.SetActive(false);
        }
    }
}