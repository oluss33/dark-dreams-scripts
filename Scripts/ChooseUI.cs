using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class ChooseUI : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(true);
            if(!File.Exists(Application.persistentDataPath + "/save.cfg"))
            {
                transform.Find("ContinueGameBTN").GetComponent<Button>().interactable = false;
            }
        }

        public void NewGame()
        {
            gameObject.SetActive(false);
        }

        public void ContinueGame()
        {
            Saving.SavingSystem.Load();
            gameObject.SetActive(false);
        }
    }
}
