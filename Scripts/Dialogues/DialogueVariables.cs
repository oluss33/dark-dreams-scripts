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
    public static class DialogueVariables
    {

        public static void Sync(Story story)
        {
            Set(story, "woody_items", Player.Player.main.inventory.inventory.HasItem(Scripts.Inventory.ItemsList.instance.spiderHeart));
        }

        private static void Set(Story story, string name, object value)
        {
            if(story.variablesState.GlobalVariableExistsWithName(name))
            {
                story.variablesState[name] = value;
            }
        }
    }
}