using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Player
{
    public class UI_Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string skillName;
        public string skillDescription;
        public int skillCost;


        public void OnPointerEnter(PointerEventData eventData)
        {
            InfoUI.instance.Setup(skillName, skillDescription, skillCost);
            InfoUI.instance.UI.transform.position = Input.mousePosition;
            InfoUI.instance.UI.SetActive(true);
            Debug.Log("kurwa mac");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InfoUI.instance.UI.SetActive(false);
            Debug.Log("XD mac");
        }

    }
}
