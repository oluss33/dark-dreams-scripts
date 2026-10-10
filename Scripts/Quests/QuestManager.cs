using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Quest
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }
        public Quest currentQuest;
        public GameObject questUI;
        public GameObject questsPanel;
        public Text nameText;
        public Text descriptionText;
     //   bool isQuestUIOpened = false;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(this);
                Debug.LogError("More than 1 QuestManager instance!");
                return;
            }
        }


        public void GiveQuest(Quest quest) {
            questUI.SetActive(true);
            currentQuest = quest;
            if (Language.LanguageManager.instance.en)
            {
                nameText.text = quest.questName;
                descriptionText.text = quest.description;
            }
            if (Language.LanguageManager.instance.pl)
            {
                nameText.text = quest._plQuestName;
                descriptionText.text = quest._plDescription;
            }
            quest.isActivated = true;
            LeanTween.moveLocalX(questUI, 300, 0.5f);
            questUI.GetComponent<AudioSource>().Play();
            Invoke("HideQuest", 4f);
        }
 
        public void HideQuest() {
            questUI.GetComponent<AudioSource>().Play();
            LeanTween.moveLocalX(questUI, 515, 0.5f).setOnComplete(Hide);
        }

        void Hide()
        {
            questUI.SetActive(false);
        }
        
        public int GetQuestIndex(Quest quest)
        {
            return quest.questData.questIndex;
        }

        public bool IsQuestActivated(Quest quest) {
            return quest.isActivated;
        }

        public int SetQuestIndex(Quest quest, int index)
        {
            return quest.questData.SetQuestIndex(index);
        }
    }
}