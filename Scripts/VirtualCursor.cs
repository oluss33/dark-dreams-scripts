using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCursor : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;

    private void Update()
    {
        transform.localScale = Vector3.one * (1f / canvas.localScale.x);
    }
}
