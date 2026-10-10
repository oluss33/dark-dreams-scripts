using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.ConceptArts
{
    public class ConceptArt : MonoBehaviour
    {
        public bool isUnlocked = false;

        Image btnImg;
        Sprite lockedSprite;
        public Sprite unlockedSprite;
        public Button btn;

        public GameObject conceptToOpen;

        private void Start()
        {
            btnImg = GetComponent<Image>();
            btn = GetComponent<Button>();
            lockedSprite = Resources.Load<Sprite>("Textures/UI/Concept Arts/padlock");
            if (!isUnlocked)
            {
                // concept art is locked
                btnImg.sprite = lockedSprite;
                btn.interactable = false;
            }
            if(isUnlocked)
            {
                btnImg.sprite = unlockedSprite;
                btn.interactable = true;
            }
        }

        public void Interact()
        {
            conceptToOpen.SetActive(true);
            ConceptManager.Instance.conceptArtsUI.SetActive(true);
        }

        public void Back()
        {
            ConceptManager.Instance.conceptArtsUI.SetActive(false);
            conceptToOpen.SetActive(false);

        }
    }
}