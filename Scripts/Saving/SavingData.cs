using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Saving
{
    public class SavingData
    {
        public List<Quest.Quest> unlockedQuests = new List<Quest.Quest>();
        public List<Inventory.Item> itemsList = new List<Inventory.Item>();
        public Vector3 position = Vector3.zero;
        public Vector2 lastCheckpointPos = Vector2.zero;
        public float currentHp;
        public float curBodyHp;
        public float curLegsHp;
        public float maxHp;
        public float currentPd;
        public float stamina;
    }
}