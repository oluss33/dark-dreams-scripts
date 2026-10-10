using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageFadeEffect : MonoBehaviour
{
    Image image;
    public float fadeSpeed = 2;

    public bool FadingIn;
    public bool HasDefault = false;
    public float defaultOpaque;
    float opaque = 0;

    void Awake()
    {
        if(HasDefault == false)
        {
            defaultOpaque = GetOpaque() * 100;
            HasDefault = true;
        }
    }
    void OnEnable()
    {
        if(HasDefault == true)
        {
            ChangeOpaque(0);
            opaque = 0;
            FadingIn = true;
        }
        
    }
    void OnDisable()
    {
        if(HasDefault == false)
        {
            defaultOpaque = GetOpaque();
            HasDefault = true;
        }
        ChangeOpaque(0);
        FadingIn = false;
    }

    void FixedUpdate()
    {
        if(FadingIn == true)
        {
            if(opaque < defaultOpaque)
            {
                opaque += fadeSpeed * Time.deltaTime;
                ChangeOpaque(opaque);
            }
            else
            {
                ChangeOpaque(defaultOpaque);
                FadingIn = false;
            }
        }
    }

    void ChangeOpaque(float a)
    {
        Image image = GetComponent<Image>();
        Text _text = GetComponent<Text>();  
        TextMeshProUGUI _textPRO = GetComponent<TextMeshProUGUI>();

        if(image != null)
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
                
        
        if(_text != null) 
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, a);
                
        
        if(_textPRO != null)
            _textPRO.color = new Color(_textPRO.color.r, _textPRO.color.g, _textPRO.color.b, a);

    }

    float GetOpaque()
    {
        Image image = GetComponent<Image>();
        Text _text = GetComponent<Text>();  
        TextMeshProUGUI _textPRO = GetComponent<TextMeshProUGUI>();

        if(image != null)
            return image.color.a;
        else if (_text != null) 
            return _text.color.a;
        else if(_textPRO != null)
            return _textPRO.color.a;
        else return -1;
    }


}
