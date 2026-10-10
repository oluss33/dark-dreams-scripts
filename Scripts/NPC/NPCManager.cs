using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Scripts.NPC
{
    [DefaultExecutionOrder(10000)]
    public class NPCManager : MonoBehaviour, IEnumerable
    {
        Dictionary<string, NPC> npcMap = new Dictionary<string, NPC>();

        public static NPCManager instance;

        private void Awake()
        {
            if (instance == null) instance = this;
            else { Destroy(this); return; }
            npcMap.Add("Amanda", new NPC("Amanda", 0));
            npcMap.Add("Boy", new NPC("Boy", 0));
            npcMap.Add("Old man", new NPC("Old man", 0));
            npcMap.Add("Ghost", new NPC("Ghost", 0));
            npcMap.Add("Woody", new NPC("Woody", 0, true));
        }

        public NPC this[string name] 
        {
            get => npcMap[name];
        }

        public int NumberOfNPC()
        {
            return npcMap.Count;
        }

        public IEnumerator GetEnumerator()
        {
            return npcMap.GetEnumerator();
        }
    }
}
