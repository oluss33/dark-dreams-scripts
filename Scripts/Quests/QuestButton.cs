using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.Quest {
    public class QuestButton : MonoBehaviour
    {
        public Quest quest;
        public Text questName;
        public Text description;
        public Image image;

        void Start() {
            /*questName.text = quest.questName;
            questDescription.text = quest.description;
            if(quest.questType == Quest.QuestType.SIDE) {
                questImage.sprite = Resources.Load<Sprite>("Quests/side_quest");
            }
            if(quest.questType == Quest.QuestType.MAIN) {
                questImage.sprite = Resources.Load<Sprite>("Quests/main_quest");
            }*/
        }

        public void Setup(Quest quest, string name, string description)
        {
            this.quest = quest;
            this.questName.text = name;
            this.description.text = description;
            if (quest.questType == Quest.QuestType.MAIN)
                this.image.sprite = Resources.Load<Sprite>("Quests/main_quest");
            else
                this.image.sprite = Resources.Load<Sprite>("Quests/side_quest");
        }
    }
}
