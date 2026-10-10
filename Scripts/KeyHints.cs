using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class KeyHints : MonoBehaviour
    {
        public GameObject hintPrefab;

        public List<GameObject> activeHints = new List<GameObject>();

        public static KeyHints instance { get; private set; }

        private GameObject _interactHint;
        private GameObject _talkHint;
        private GameObject _lootHint;

        private bool combat_created = false;
        private bool regular_createad = false;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }
        
        private void Start()
        {
            GenerateBasicHints();
        }

        private void Update()
        {
            if (Player.Player.main.isFighting)
            {
                if (!combat_created)
                {
                    combat_created = true;
                    regular_createad = false;
                    GenerateCombatHints();
                    
                }
            }
            else
            {
                if (!regular_createad)
                {
                    regular_createad = true;
                    combat_created = false;
                    GenerateBasicHints();
                    
                }
            }
        }

        private void GenerateCombatHints()
        {
            if (activeHints.Count > 0)
                for (int i = 0; i < activeHints.Count; i++)
                {
                    Destroy(activeHints[i].gameObject);
                }
            activeHints.Clear();

            string[,] hints = new string[,]
            {
                { "V", "Attack" },
                { "F", "Block" },
                { "C", "Dodge" },
                { "Q", "Parry" }
            };

            for (int i = 0; i < hints.GetLength(0); i++)
            {
                GameObject hint = Instantiate(hintPrefab, transform);
                var keyHint = hint.GetComponent<KeyHint>();
                keyHint.SetKeyboardHint(hints[i, 0], hints[i, 1]);
                activeHints.Add(hint);
            }
        }

        private void GenerateBasicHints()
        {
            if (activeHints.Count > 0)
                for (int i = 0; i < activeHints.Count; i++)
                {
                    Destroy(activeHints[i].gameObject);
                }
            activeHints.Clear();

            string[,] hints = new string[,]
            {
                { "AD", "Move" },
                { "Space", "Jump" },
                { "M", "Diary" },
                { "Esc", "Pause" }
            };
            
            for (int i = 0; i < hints.GetLength(0); i++)
            {
                GameObject hint = Instantiate(hintPrefab, transform);
                var keyHint = hint.GetComponent<KeyHint>();
                keyHint.SetKeyboardHint(hints[i, 0], hints[i, 1]);
                activeHints.Add(hint);
            }
        }

        public void ShowInteractHint()
        {
            if (_interactHint != null) return;
            string[] hint = new string[] { "E", "Interact" };
            
            GameObject _hint = Instantiate(hintPrefab, transform);
            _interactHint = _hint;
            var keyHint = _hint.GetComponent<KeyHint>();
            keyHint.SetKeyboardHint(hint[0], hint[1]);
            activeHints.Add(_hint);
        }

        public void ShowTalkHint()
        {
            if (_talkHint != null) return;

            string[] hint = new string[] {"F", "Talk"};
             GameObject _hint = Instantiate(hintPrefab, transform);
            _talkHint = _hint;
            var keyHint = _hint.GetComponent<KeyHint>();
            keyHint.SetKeyboardHint(hint[0], hint[1]);
            activeHints.Add(_hint);
        }

        public void ShowLootHint()
        {
            if (_lootHint != null) return;
            string[] hint = new string[] { "G", "Loot Enemy"};
            GameObject _hint = Instantiate(hintPrefab, transform);
            _lootHint = _hint;
            var keyHint = _hint.GetComponent<KeyHint>();
            keyHint.SetKeyboardHint(hint[0], hint[1]);
            activeHints.Add(_hint);
        }

        public void HideLootHint()
        {
            if (_lootHint != null)
            {
                activeHints.Remove(_lootHint);
                Destroy(_lootHint);
            }
        }

        public void HideInteractHint()
        {
            if (_interactHint != null)
            {
                activeHints.Remove(_interactHint);
                Destroy(_interactHint);
            }
        }

        public void HideTalkHint()
        {
            if (_talkHint != null)
            {
                activeHints.Remove(_talkHint);
                Destroy(_talkHint);
            }
        }
    }
}