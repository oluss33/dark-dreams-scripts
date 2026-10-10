using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilityButton : MonoBehaviour
{
    public Image icon;
    public Image cooldownImg;

    public void Setup(Sprite icon)
    {
        this.icon.sprite = icon;
    }
}
