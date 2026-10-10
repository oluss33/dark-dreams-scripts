using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Scripts.Saving
{
    public static class SavingSystem
    {

        public static void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/save.cfg"))
            {
                Debug.Log("elo");
                string saveString = File.ReadAllText(Application.persistentDataPath + "/save.cfg");

                SavingData data = JsonUtility.FromJson<SavingData>(saveString);

                for (int i = 0; i < data.itemsList.Count; i++)
                {
                    // TODO: fix
                    //Player.Player.main.inventory.AddItem(data.itemsList[i]);
                    Debug.Log("Loaded " + data.itemsList[i]);
                }

                for (int i = 0; i < data.unlockedQuests.Count; i++)
                {
                  //  Player.Player.main.inventory.(data.itemsList[i]);
                    Debug.Log("Loaded " + data.unlockedQuests[i]);
                }

                Player.Player.main.transform.position = data.position;
               // GameManager.Instance.lastCheckpointPos = data.lastCheckpointPos; TODO: fix it after demo ver

                Player.PlayerHealth.instance.currentHp = data.currentHp;
                Player.PlayerStats.instance.maxHp = data.maxHp;
                Player.PlayerStats.instance.exp = data.currentPd;
                Player.PlayerStats.instance.currentStamina = data.stamina;
            }
            else
            {
                Debug.Log("No save found!");
            }
        }
    }
}