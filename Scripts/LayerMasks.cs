using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LayerMasks : MonoBehaviour
{
    [Header("Base")]
    public LayerMask playerMask;
    public LayerMask groundMask;
    public LayerMask enemyMask;
    [Space(1)]
    public LayerMask doorsMask;
    public LayerMask lineMask;
    public LayerMask interactableMask;
    
    public static LayerMasks instance { get; private set; }

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            Debug.LogError("More than 1 LayerMasks Manager!");
        }
    }
}
