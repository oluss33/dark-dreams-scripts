using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scripts.Abilities;

namespace Scripts.Player
{
    public class PlayerSkills
    {

        Player player = Player.main;

        public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;

        public class OnSkillUnlockedEventArgs : EventArgs
        {
            public SkillType skillType;
        }

        public enum SkillType
        {
            None,
           
            // 1
            Health_1,
            Health_2,
            
            Healing_1,
            Healing_2,
            
            // 2
            Protection_1,
            Protection_2,
            
            Dmg_1,
            Dmg_2,
            
            // 3
            Dodge_1,
            Dodge_2,
            
            Secrets_1,
            Secrets_2,
            
            // 4
            Jump_1,
            Jump_2,
            
            Stamina_1,
            Stamina_2,
            
            // 5
            Luck_1,
            Luck_2,
            
            Speed_1,
            Speed_2,
            
            // TODO: Super-Skills
            Ability_1,
            Ability_2,
            Ability_3,
            Ability_4,
            Ability_5

        }

        public List<SkillType> unlockedSkillTypes;

        public PlayerSkills()
        {
            unlockedSkillTypes = new List<SkillType>();
        }

        public void UnlockSkill(SkillType skillType)
        {
            if (!IsSkillUnlocked(skillType))
            {
                unlockedSkillTypes.Add(skillType);
                OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
            }
        }

        public bool IsSkillUnlocked(SkillType skillType)
        {
            return unlockedSkillTypes.Contains(skillType);
        }

        public List<SkillType> GetSkillRequirment(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.Health_2: return new List<SkillType> { SkillType.Health_1 };
                case SkillType.Healing_2: return new List<SkillType> { SkillType.Healing_1 };
                case SkillType.Protection_2: return new List<SkillType> { SkillType.Protection_1 };
                case SkillType.Dmg_2: return new List<SkillType> { SkillType.Dmg_1 };
                case SkillType.Dodge_2: return new List<SkillType> { SkillType.Dodge_1 };
                case SkillType.Secrets_2: return new List<SkillType> { SkillType.Secrets_1 };
                case SkillType.Jump_2: return new List<SkillType> { SkillType.Jump_1 };
                case SkillType.Stamina_2: return new List<SkillType> { SkillType.Stamina_1 };
                case SkillType.Luck_2: return new List<SkillType> { SkillType.Luck_1 };
                case SkillType.Speed_2: return new List<SkillType> { SkillType.Speed_1 };
                
                // SuperSkills
                case SkillType.Ability_1:
                    return new List<SkillType> { SkillType.Health_2, SkillType.Healing_2 };
                
            }
            return new List<SkillType>{ SkillType.None };
        }

        public float GetSkillCost(SkillType skillType)
        {
            return 1;
            /* switch (skillType)
             {
                 default: return 1;
             }*/
        }

        public bool CanUnlock(SkillType skillType)
        {
            var skillRequirments = GetSkillRequirment(skillType);

           /* if (IsSkillUnlocked(skillType))
            {
                return false;
            }*/


           bool canUnlock = false;
           
           foreach (var skill in skillRequirments)
           {
               if (skill != SkillType.None)
               {
                   if (IsSkillUnlocked(skill))
                   {
                       canUnlock = true;
                   }
                   else
                   {
                       canUnlock = false;
                   }
               }
               else
               {
                   canUnlock = true;
               }
           }

           return canUnlock;
        }

        public void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
        {
//            PlayerCombat combat = player.GetComponent<PlayerCombat>();
            switch (e.skillType)
            {
                case PlayerSkills.SkillType.Healing_1:
                    Debug.Log("Unlocked");
                    break;
                case PlayerSkills.SkillType.Healing_2:
                    Debug.Log("Unlocked");
                    break;
                case PlayerSkills.SkillType.Health_1:
                    Debug.Log("Unlocked");
                    break;
                case PlayerSkills.SkillType.Health_2:
                    Debug.Log("Unlocked");
                    break;
                case PlayerSkills.SkillType.Ability_1:
                    Debug.Log("unlocked ability");
                    break;
                default:
                    Debug.Log("@default: unlocked");
                    break;
            }
        }

        public bool TryUnlockSkill(SkillType skillType)
        {
            if (CanUnlock(skillType))
            {
                if (!IsSkillUnlocked(skillType))
                {
                    Debug.Log(skillType);
                     Debug.Log(PlayerStats.instance.exp);
                    if (PlayerStats.instance.exp >= GetSkillCost(skillType))
                    {
                        PlayerStats.instance.DecreaseExp(GetSkillCost(skillType));
                        UnlockSkill(skillType);
                        return true;
                    }
                    else
                    {
                        Tooltip.TooltipWarning.ShowTooltip_Static("You don't have enough points!");
                        return false;
                    }
                }
                else
                {
                    Tooltip.TooltipWarning.ShowTooltip_Static("You already unlocked this skill!");
                    return false;
                }

            }
            else
            {
                Tooltip.TooltipWarning.ShowTooltip_Static("You can't unlock this skill!");
                return false;
            }

        }
    }
}