using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Quest {
    public class QuestFinisher : MonoBehaviour
    {
        public Quest quest;
        public Quest newQuest;
        public Text questTxt;
        public Text questTitleTxt;
        public GameObject questsParent;
        public ConceptArts.ConceptArt conceptArtToUnlock;

        void OnTriggerEnter2D(Collider2D col) {
            if(col.CompareTag(Tags.Player)) {
                FinishQuest();
            }
        } 

        public void FinishQuest() {
            if (conceptArtToUnlock != null)
            {
                conceptArtToUnlock.isUnlocked = true;
                Player.Player.main.unlockedConceptArts.Add(conceptArtToUnlock);
                if(Player.Player.main.unlockedConceptArts.Count == ConceptManager.Instance.buttons.Length)
                {
                    GameJolt.API.Trophies.Unlock(158116, (bool succes) =>
                    {
                        if (succes)
                        {
                            Debug.Log("Trophy unlocked!");
                        }
                        else
                        {
                            Debug.Log("kurwa");
                        }
                    });
                }
            }
            if(quest.isActivated != true) return;
            if (quest.isFinished) return;
            quest.isFinished = true;
            quest.isActivated = false;
            newQuest.isActivated = true;
            Player.Player.main.unlockedQuests.Add(quest);
            StartCoroutine(Finisher());
        }

        IEnumerator Finisher() {
            questTxt.text = "Quest " + quest.questName + " finished!";
            questTitleTxt.text = quest.questName.ToString();
            questsParent.SetActive(true);
            yield return new WaitForSeconds(3f);
            questsParent.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            //Dialogues.DialogueManager.instance.AddQuest(newQuest);
            Destroy(this);
            
        }
    }
}
