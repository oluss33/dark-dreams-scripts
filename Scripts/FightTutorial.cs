using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    public class FightTutorial : MonoBehaviour, Openable
    {

        public static FightTutorial instance { get; private set; }

        public GameObject ui;
        public Text tutorialText;
        public Sprite[] sprites;
        public Image image;
        public GameObject nextBtn;
        public GameObject backBtn;
        public GameObject closeBtn;

        public string[] tutorialTexts;

        private int index = 0;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this);
                Debug.LogError("More than 1 FightTutorial instance!");
                return;
            }
        }

        void Start()
        {
            ui.SetActive(false);
            backBtn.SetActive(false);
            nextBtn.SetActive(true);
            closeBtn.SetActive(false);
        }

        public void Actviate()
        {
            ui.SetActive(true);
            image.sprite = sprites[0];
            Time.timeScale = 0f;
            tutorialText.text = tutorialTexts[0];
        }

        public void Next()
        {
            if (index < sprites.Length - 1)
            {
                index++;
                image.sprite = sprites[index];
                tutorialText.text = tutorialTexts[index];
            }

            if (index == sprites.Length - 1)
            {
                nextBtn.SetActive(false);
                closeBtn.SetActive(true);
            }

            if (index > 0)
            {
                backBtn.SetActive(false);
            }
        }

        public void Back()
        {
            if (index > 0)
            {
                index--;
                image.sprite = sprites[index];
                tutorialText.text = tutorialTexts[index];
            }

            if (index == 0)
            {
                backBtn.SetActive(false);
            }

            if (index < sprites.Length - 1)
            {
                nextBtn.SetActive(true);
                closeBtn.SetActive(false);
            }
        }

        public void Close()
        {
            ui.SetActive(false);
            Time.timeScale = 1f;
            index = 0;
            image.sprite = sprites[0];
            tutorialText.text = tutorialTexts[0];
            backBtn.SetActive(false);
            nextBtn.SetActive(true);
            closeBtn.SetActive(false);
        }

        public bool IsOpened()
        {
            return ui.activeSelf;
        }
    }
}
