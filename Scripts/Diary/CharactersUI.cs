using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Diary
{
    public class CharactersUI : MonoBehaviour
    {
        [SerializeField] GameObject[] characters;
        public int index = 0;
        public int canUnlockToIndex = 0;

        public void Start()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].SetActive(false);
            }
            characters[0].SetActive(true);
        }

        public void NextCharacter()
        {
            if (index < canUnlockToIndex)
            {
                index++;
                for (int i = 0; i < characters.Length; i++)
                {
                    characters[i].SetActive(false);
                }
                characters[index].SetActive(true);
            }
        }

        public void PreviousCharacter()
        {

            if (index > 0)
            {
                index--;
                for (int i = 0; i < characters.Length; i++)
                {
                    characters[i].SetActive(false);
                }
                characters[index].SetActive(true);

            }
        }
    }
}