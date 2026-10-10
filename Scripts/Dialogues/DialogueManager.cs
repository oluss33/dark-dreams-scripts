using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Ink.UnityIntegration;
using Scripts.CameraExtras;
using Scripts.Sounds;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Scripts.Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance { get; private set; }
        public bool isTalking { get; private set; }
        
        private InkFile inkFile;
        [SerializeField] private Animator portraitAnim;
        [SerializeField] private Animator buttonsAnim;

        [Header("UI")] 
        [SerializeField] public GameObject UI;
        [SerializeField] private Text dialogueText;

        [SerializeField] private Button[] choiceButtons;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button endButton;
        private Story story;
        private NPC.NPC npc;

        private const string SPEAKER_TAG = "speaker";
        private const string PORTRAIT_TAG = "portrait";
        private const string LAYOUT_TAG = "layout";

        private bool showChoices = false;

        private AudioClip[] altenSounds;
        private AudioClip[] amandaSounds;
        private string path = "Dialogues/Sounds/";

        private bool canGoNext = true;

        private Coroutine typingCoroutine;
        
        private void Awake()
        {
            if (instance == null) instance = this;
            else
            {
                Destroy(this.gameObject);
                Debug.LogError("More than 1 DialogueManager instance");
            }

            altenSounds = Resources.LoadAll<AudioClip>(path + "Alten");
            amandaSounds = Resources.LoadAll<AudioClip>(path + "Amanda");
            
            print("@DialogueManager: Loaded " + altenSounds.Length + " Alten sounds.");
            print("@DialogueManager: Loaded " + amandaSounds.Length + " Amanda sounds.");

        }

        private void Start()
        {
            UI.gameObject.SetActive(false);
            isTalking = false;
        }

        public void StartDialogue(InkFile inkFile, NPC.NPC npc)
        {
            if (isTalking) return;
            if (this.inkFile != inkFile || story == null)
            {
                Debug.Log("tworze nowa historie");
                this.inkFile = inkFile;
                this.npc = npc;
                story = new Story(inkFile.storyJson);
                DialogueExternalFunctions.instance.Bind(story);
            }
            DialogueVariables.Sync(story);

            /*if (this.npc.restartDialogueOnRepeat)
            {
                story = new Story(inkAsset.text);
                DialogueExternalFunctions.instance.Bind(story);
            }*/

            portraitAnim.Play("default");
            buttonsAnim.Play("default");

            nextButton.gameObject.SetActive(false);
            endButton.gameObject.SetActive(false);

            UI.SetActive(true);

            isTalking = true;

            UpdateUI();
        }

        IEnumerator TypeText(string finalText)
        {
            canGoNext = false;
            string t = "";
            float time = 0.05f;
            foreach(char letter in finalText)
            {
                t += letter;
                dialogueText.text = t;
                yield return new WaitForSeconds(time);
            }
            canGoNext = true;
        }

        private void UpdateUI()
        {
            // Disable all of the choice buttons
            for (int j = 0; j < choiceButtons.Length; j++)
            {
                choiceButtons[j].gameObject.SetActive(false);
            }

            if (story.canContinue) story.Continue();
            //dialogueText.text = story.currentText;
            typingCoroutine = StartCoroutine(TypeText(story.currentText));

            HandleTags(story.currentTags);

            // Check if we have available choice
            if (story.currentChoices.Count > 0)
            {
                // Yes
                showChoices = true;
                nextButton.gameObject.SetActive(true);
            }
            else
            {
                showChoices = false;
                // No
                // Can we continue?
                StateUpdate();
            }
        }

        private void StateUpdate()
        {
            if (story.canContinue)
            {
                nextButton.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("@DialogueManager.CanContinue: False");
                endButton.gameObject.SetActive(true);
            }
        }

        private void HandleTags(List<string> currentTags)
        {
            foreach (string tag in currentTags)
            {
                string[] splitTag = tag.Split(':');

                if (splitTag.Length != 2)
                {
                    Debug.Log("@DialogueManager: Something went wrong with Tags");
                }

                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();

                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        Debug.Log("speaker: " + tagValue);
                        portraitAnim.Play(tagValue);
                        
                        break;
                    case PORTRAIT_TAG:
                        Debug.Log("portrait: " + tagValue);
                        portraitAnim.Play(tagValue);
                        break;
                    case LAYOUT_TAG:
                        Debug.Log("layout: " + tagValue);
                        break;
                    default:
                        break;
                }
                PlaySentence(tagValue);
                buttonsAnim.Play("default");

            }
        }

        private void PlaySentence(string tagValue)
        {
            AudioClip clip;
            print("playing sentence");
            switch (tagValue)
            {
                case "default":
                case var x when x.StartsWith("alten"):
                    clip = altenSounds[Random.Range(0, altenSounds.Length)];
                    Player.Player.main.source.PlayOneShot(clip);
                    break;
                case "amanda":
                    clip = amandaSounds[Random.Range(0, amandaSounds.Length)];
                    Player.Player.main.source.PlayOneShot(clip);
                    break;
            }

        }

        public void Next()
        {
            if(!canGoNext)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = story.currentText;
                canGoNext = true;
                return;
            }
            nextButton.gameObject.SetActive(false);
            if (showChoices)
            {
                //portraitAnim.Play("default");
                //buttonsAnim.Play("default");

                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    // Loop, set dialogue choice variables
                    Choice c = story.currentChoices[i];
                    Button b = choiceButtons[i];

                    // Show dialogue choices
                    b.gameObject.SetActive(true);
                    b.GetComponentInChildren<Text>().text = c.text;
                }
            }
            else
            {
                UpdateUI();
            }
        }

        public void End()
        {
            isTalking = false;
            CinemachineEffects.instance.Zoom(CinemachineEffects.instance.startSize, 0.5f);
            Player.Player.main.source.PlayOneShot(SoundsManager.swoosh);
            UI.SetActive(false);
        }

        public void ChooseChoiceIndex(int i)
        {
            for (int j = 0; j < choiceButtons.Length; j++)
            {
                choiceButtons[j].gameObject.SetActive(false);
            }

            //portraitAnim.Play("default");
            //buttonsAnim.Play("default");

            //string choiceText = story.currentChoices[i].text;
            story.ChooseChoiceIndex(i);
            //dialogueText.text = choiceText;

            // Can we continue?
            UpdateUI();
        }
    }
}