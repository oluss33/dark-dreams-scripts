using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Abilities;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{
    public AbilityManager manager;

    public GameObject[] buttons;

    private List<Ability> _abilities;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateUI(List<Ability> abilities)
    {
        _abilities = abilities;
        for (int i = 0; i < _abilities.Count; i++)
        {
            buttons[i].GetComponent<AbilityButton>().Setup(_abilities[i].icon);
            if(!abilities[i].isUnlocked) buttons[i].SetActive(false);
            else Debug.Log("Ability Odblokowane");
        }
    }
   
    public void SetUICooldown(int index, float value, float maxValue)
    {
        buttons[index].GetComponent<AbilityButton>().cooldownImg.fillAmount = value / maxValue;
    }
    
}
