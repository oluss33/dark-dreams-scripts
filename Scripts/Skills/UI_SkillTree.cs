using Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Skills
{
    public class UI_SkillTree : MonoBehaviour
    {

        private PlayerSkills playerSkills;
        [SerializeField] private SkillUnlockPath[] unlockPath;
        List<SkillButton> buttonsList = new List<SkillButton>();

        [SerializeField] private Color lockedColor;
        [SerializeField] private Color unlockableColor;
        [SerializeField] private Color unlockedColor;

        [Header("Buttons")] 
        // 1
        [SerializeField] private Transform[] healthBTN;
        [SerializeField] private Transform[] healingBTN;
        [SerializeField] private Transform ability1;

        // 2
        [SerializeField] private Transform[] protectionBTN; 
        [SerializeField] private Transform[] dmgBTN;
        [SerializeField]private Transform ability2;


        // 3
        [SerializeField] private Transform[] dodgeBTN;
        [SerializeField] private Transform[] secretsBTN;
        [SerializeField]private Transform ability3;


        // 4
        [SerializeField] private Transform[] jumpBTN;
        [SerializeField] private Transform[] staminaBTN;
        [SerializeField] private Transform ability4;
        
        // 5
        [SerializeField] private Transform[] luckBTN;
        [SerializeField] private Transform[] speedBTN;
        [SerializeField] private Transform ability5;

        private void Start()
        {
            
            healingBTN[0].GetComponent<Button>().onClick.AddListener(delegate()
            {
                playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Healing_1);
            });
            
            healingBTN[1].GetComponent<Button>().onClick.AddListener(delegate()
            {
                playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Healing_2);
            });

            healthBTN[0].GetComponent<Button>().onClick.AddListener(() =>
            {
                playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Health_1);
            });
            
            healthBTN[1].GetComponent<Button>().onClick.AddListener(() =>
            {
                playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Health_2);
            });
            
            ability1.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("kurwaaa");
                playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Ability_1);
            });
            
            
            /*     transform.Find("Damage1Btn").GetComponent<Button>().onClick.AddListener(delegate ()
                 {
                     playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Damage_1);
                 });

                 transform.Find("Damage2Btn").GetComponent<Button>().onClick.AddListener(delegate ()
                 {
                     playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Damage_2);
                 });

                 transform.Find("Health1Btn").GetComponent<Button>().onClick.AddListener(delegate ()
                 {
                     playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Health_1);
                 });

                 transform.Find("Health2Btn").GetComponent<Button>().onClick.AddListener(delegate ()
                 {
                     playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Health_2);
                 });*/

            UpdateVisuals();
        }

        public void SetPlayerSkills(PlayerSkills playerSkills)
        {
            this.playerSkills = playerSkills;
            
            buttonsList.Add(new SkillButton(healingBTN[0].transform, playerSkills, PlayerSkills.SkillType.Healing_1, healingBTN[0].GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
            buttonsList.Add(new SkillButton(healingBTN[1].transform, playerSkills, PlayerSkills.SkillType.Healing_2, healingBTN[1].GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
                    
            buttonsList.Add(new SkillButton(healthBTN[0].transform, playerSkills, PlayerSkills.SkillType.Health_1, healthBTN[0].GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
            buttonsList.Add(new SkillButton(healthBTN[1].transform, playerSkills, PlayerSkills.SkillType.Health_2, healthBTN[1].GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));

            /*
            buttonsList.Add(new SkillButton(transform.Find("Damage1Btn"), playerSkills, PlayerSkills.SkillType.Damage_1, transform.Find("Damage1Btn").GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
          //  transform.Find("Damage1Btn").GetComponent<UI_Skill>().Setup("Damage", "Boost your damage",1);
            buttonsList.Add(new SkillButton(transform.Find("Damage2Btn"), playerSkills, PlayerSkills.SkillType.Damage_2, transform.Find("Damage2Btn").GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
           // transform.Find("Damage2Btn").GetComponent<UI_Skill>().Setup("Damage", "Boost your damage", 1);

            buttonsList.Add(new SkillButton(transform.Find("Health1Btn"), playerSkills, PlayerSkills.SkillType.Health_1, transform.Find("Health1Btn").GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
            //transform.Find("Health1Btn").GetComponent<UI_Skill>().Setup("Health", "Boost your max health", 1);

            buttonsList.Add(new SkillButton(transform.Find("Stamina1Btn"), playerSkills, PlayerSkills.SkillType.Stamina_1, transform.Find("Stamina1Btn").GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
            
            buttonsList.Add(new SkillButton(transform.Find("Stamina2Btn"), playerSkills, PlayerSkills.SkillType.Stamina_2, transform.Find("Stamina2Btn").GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));

            buttonsList.Add(new SkillButton(transform.Find("Dodge1Btn"), playerSkills, PlayerSkills.SkillType.Dodge_1, transform.Find("Dodge1Btn").GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
            buttonsList.Add(new SkillButton(transform.Find("Dodge2Btn"), playerSkills, PlayerSkills.SkillType.Dodge_2, transform.Find("Dodge2Btn").GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));

            buttonsList.Add(new SkillButton(transform.Find("Jump1Btn"), playerSkills, PlayerSkills.SkillType.Jump_1, transform.Find("Jump1Btn").GetComponent<Outline>(), lockedColor, unlockableColor, unlockedColor));
            */
            
            playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
            UpdateVisuals();
        }

        private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
        {
            UpdateVisuals();
        }

        void UpdateVisuals()
        {
            foreach (SkillButton b in buttonsList)
            {
                b.UpdateVisual();
            }
            
            /*
            foreach (SkillUnlockPath path in unlockPath)
            {
                foreach (Image linkImage in path.images)
                {
                    linkImage.color = new Color(0.5f, 0.5f, 0.5f);
                }
            }

            foreach (SkillUnlockPath path in unlockPath)
            {
                if (playerSkills.IsSkillUnlocked(path.skillType) || playerSkills.CanUnlock(path.skillType))
                {
                    foreach (Image linkImage in path.images)
                    {
                        linkImage.color = Color.white;
                    }
                }
            }
            */
        }

        class SkillButton
        {
            Transform transform;
            Outline outline;

            PlayerSkills playerSkills;
            PlayerSkills.SkillType skillType;

            Color lockedColor;
            Color unlockableColor;
            Color unlockedColor;

            public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType, Outline outline, Color lockedColor, Color unlockableColor, Color unlockedColor)
            {
                this.transform = transform;
                this.playerSkills = playerSkills;
                this.skillType = skillType;
                this.outline = outline;
                this.lockedColor = lockedColor;
                this.unlockableColor = unlockableColor;
                this.unlockedColor = unlockedColor;

                transform.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    playerSkills.TryUnlockSkill(skillType);
                });
            }

            public void UpdateVisual()
            {
                if (playerSkills.IsSkillUnlocked(skillType))
                {
                    Debug.Log("Zmieniam kolor na odblokowany");
                    outline.effectColor = unlockedColor;
                }
                else
                {
                    if (!(playerSkills.CanUnlock(skillType)))
                    {
                        outline.effectColor = lockedColor;
                    }
                    else
                    {
                        Debug.Log("Zmieniam kolor na mozliwy do odblokowania");
                        outline.effectColor = unlockableColor;
                    }
                }
            }
        }

        [System.Serializable]
        public class SkillUnlockPath
        {
            public PlayerSkills.SkillType skillType;
            public Image[] images;
        }
    }
}