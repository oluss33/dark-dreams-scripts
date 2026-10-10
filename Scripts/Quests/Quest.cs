using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Quest
{
    [System.Serializable]
    public class QuestData
    {
        public int questIndex;
        public int SetQuestIndex(int index)
        {
            return questIndex = index;
        }
    }

    [CreateAssetMenu(fileName =  "Quest", menuName = "CubeLand Studio/Quests/New Quest")]
    public class Quest : ScriptableObject
    {
        private void OnEnable() {
            isActivated = false;
            isCreated = false;
            isFinished = false;
        }
        [HideInInspector]
        public QuestData questData;
        public string questName;
        public string description;
        public string _plQuestName;
        public string _plDescription;
        public QuestType questType;
        public enum QuestType { SIDE, MAIN }
        public bool isActivated;
        public bool isCreated;
        public bool isFinished;
    }
}