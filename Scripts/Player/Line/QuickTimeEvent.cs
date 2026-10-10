using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuickTimeEvent : MonoBehaviour
{
    
    public static QuickTimeEvent instance { get; private set; }
    
    private Key[] keys = { Key.A, Key.B, Key.C };

    public Key keyToPress;

    public GameObject UI;

    public TMP_Text TMP;

    public float maxTime;

    public Slider timeSlider;

    public bool isActive = false;

    private UnityEvent onSucceed;
    private UnityEvent onFailed;

    private void Awake()
    {
        instance = this;
        UI.SetActive(false);
    }


    public void KeyControl()
    {
        
        // KEYBOARD & MOUSE
        var keyControl = Keyboard.current[keyToPress];

        if (keyControl != null && keyControl.wasPressedThisFrame)
        {
            onSucceed?.Invoke();
            DisableQTE();
        }
        else
        {

            foreach (var info in Keyboard.current.allKeys)
            {
                if (info != null)
                {
                    if (info.isPressed && info.keyCode != keyToPress)
                    {
                        onFailed?.Invoke();
                        DisableQTE();
                        break;
                    }
                }
            }
        }
    }
    
    private void Update()
    {
        if (isActive)
        {
            timeSlider.value -= Time.deltaTime;
            if (Keyboard.current == null) return;
            
            KeyControl();

            if (timeSlider.value <= 0)
            {
                timeSlider.value = 0;
                onFailed?.Invoke();
                DisableQTE();
            }
        }
    }

    public void GenerateQTE(float maxTime, UnityEvent onSucceed, UnityEvent onFailed, Key specificKey = Key.None)
    {
        this.maxTime = maxTime;
        this.onSucceed = onSucceed;
        this.onFailed = onFailed;
        if (specificKey != Key.None)
        {
            GenerateSpecificKey(specificKey);
        }
        else
        {
            GenerateRandomKey();
        }

        timeSlider.maxValue = maxTime;
        timeSlider.value = maxTime;
        isActive = true;
    }

    void GenerateRandomKey()
    {
        int index = Random.Range(0, keys.Length);
        keyToPress = keys[index];

        TMP.text = keys[index].ToString();
    }

    void GenerateSpecificKey(Key key)
    {
        keyToPress = key;

        TMP.text = key.ToString();
    }

    void DisableQTE()
    {
        isActive = false;
        UI.SetActive(false);
    }
}