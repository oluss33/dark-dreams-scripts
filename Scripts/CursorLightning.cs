using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLightning : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
