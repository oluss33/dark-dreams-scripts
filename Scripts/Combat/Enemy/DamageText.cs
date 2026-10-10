using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float dmg;
    TextMeshPro txt;
    void Start()
    {
        txt = GetComponent<TextMeshPro>();
        txt.text = dmg.ToString();
    }

    private void Update()
    {
        transform.Translate(Vector2.one * 6 * Time.deltaTime);
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - (0.5f * Time.deltaTime)); ;
        if(txt.color.a < 0.001f)
        {
            Destroy(gameObject);
        }
    }
}
