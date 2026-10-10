using System.Collections;
using System.Collections.Generic;
using Scripts.Language;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Quest {
    public class QuestsUI : MonoBehaviour
    {
        public Quest[] quests;
        public GameObject questPrefab;
        public Transform mainQuestsParent;
        public Transform sideQuestsParent;

        public List<GameObject> questGameObjects = new List<GameObject>();

        public void UpdateQuests() {
            for (int i = 0; i < quests.Length; i++)
            {
                if (quests[i].isActivated && !quests[i].isCreated)
                {
                    if (quests[i].questType == Quest.QuestType.MAIN)
                    {
                        GameObject questGO = Instantiate(questPrefab, Vector3.zero, Quaternion.identity,
                            mainQuestsParent);
                        if(LanguageManager.instance.en)
                            questGO.GetComponent<QuestButton>().Setup(quests[i], quests[i].questName, quests[i].description);
                        else questGO.GetComponent<QuestButton>().Setup(quests[i], quests[i]._plQuestName, quests[i]._plDescription);
                        questGameObjects.Add(questGO);
                    }
                    else if(quests[i].questType == Quest.QuestType.SIDE)
                    {
                        GameObject questGO = Instantiate(questPrefab, Vector3.zero, Quaternion.identity,
                            sideQuestsParent);
                        if(LanguageManager.instance.en)
                            questGO.GetComponent<QuestButton>().Setup(quests[i], quests[i].questName, quests[i].description);
                        else questGO.GetComponent<QuestButton>().Setup(quests[i], quests[i]._plQuestName, quests[i]._plDescription);
                        questGameObjects.Add(questGO);
                    }

                    quests[i].isCreated = true;
                }

                if (quests[i].isFinished)
                {
                    GameObject questGameObject;
                    if(LanguageManager.instance.en)
                       questGameObject  = questGameObjects.Find(g => g.GetComponent<QuestButton>().quest.questName == quests[i].questName);
                    else
                        questGameObject  = questGameObjects.Find(g => g.GetComponent<QuestButton>().quest._plQuestName == quests[i]._plQuestName);
                        
                    if (questGameObject != null)
                    {
                        Destroy(questGameObject);
                        questGameObjects.Remove(questGameObject);
                    }
                }
            }
        }
    }
}
