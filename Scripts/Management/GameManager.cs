using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.IO;

using System;
using Scripts.Inventory.ItemFunctions;

namespace Scripts
{
    [DefaultExecutionOrder(-10293)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Transform currentCheckpoint;
        public float globalLightValue;

        [SerializeField] GameObject fpsCounter;
        private bool FPS_isShown = false;

        [Header("Light Controller")]
        public UnityEngine.Rendering.Universal.Light2D globalLight;

        public GameObject playerHpBars;
        public bool usedStatue = false;
        public bool openedInventory = false;
        public bool equippedWeapon = false;

        public bool everFought = false;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(this);
                Debug.LogError("More than 1 QuestManager instance!");
                return;
            }
            
            ItemFunctionality.Init();
        }

        private void Start()
        {
//            globalLightValue = Lightning.DemoLight.instance.light2d.intensity;
            playerHpBars.SetActive(true);
            //staminaBar.SetActive(false);
            //cooldownBar.SetActive(false);
            fpsCounter.SetActive(false);
        }

        public void Save()
        {
            Saving.SavingData data = new Saving.SavingData
            {
               // itemsList = Player.Player.main.inventory.itemsList,
                unlockedQuests = Player.Player.main.unlockedQuests,
                position = Player.Player.main.transform.position,
                //lastCheckpointPos = lastCheckpointPos, TODO: change it after demo ver
                maxHp = Player.PlayerStats.instance.maxHp,
                currentHp = Player.PlayerHealth.instance.currentHp,
                currentPd = Player.PlayerStats.instance.exp,
                stamina = Player.PlayerStats.instance.currentStamina,
            };

            string jsonFile = JsonUtility.ToJson(data);
            if (File.Exists(Application.persistentDataPath + "/save.cfg"))
            {
                File.Delete(Application.persistentDataPath + "/save.cfg");
            }
            File.WriteAllText(Application.persistentDataPath + "/save.cfg", jsonFile);
        }
        private void OnApplicationQuit()
        {
            Save();
           // Player.Player.main.playerInputActions.Disable();
        }

        public void ManageFPS()
        {
            if (!FPS_isShown) fpsCounter.SetActive(true);
            else fpsCounter.SetActive(false);
            FPS_isShown = !FPS_isShown;
        }

        public List<T> FindObjectsOfTypeAll<T>()
        {
            List<T> results = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var s = SceneManager.GetSceneAt(i);
                if (s.isLoaded)
                {
                    var allGameObjects = s.GetRootGameObjects();
                    for (int j = 0; j < allGameObjects.Length; j++)
                    {
                        var go = allGameObjects[j];
                        results.AddRange(go.GetComponentsInChildren<T>(true));
                    }
                }
            }
            return results;
        }

    }
}
