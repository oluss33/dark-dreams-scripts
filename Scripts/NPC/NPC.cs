using System;

namespace Scripts.NPC
{
    [System.Serializable]
    public class NPC
    {
        public string name;
        public int reputation;
        public bool dialogueCreated = false;
        public bool restartDialogueOnRepeat;

        
        public NPC(string name, int reputation, bool restartDialogueOnRepeat = false)
        {
            this.name = name;
            this.reputation = reputation;
            this.restartDialogueOnRepeat = restartDialogueOnRepeat;
        }
    }
}
