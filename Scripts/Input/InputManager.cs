using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;


public enum GameDevice
{
    KeyboardAndMouse,
    Gamepad
}
public class InputManager : MonoBehaviour
{
    public GameDevice device;
    [SerializeField] private GameObject virtualCursorUI;
    [SerializeField] private GameObject virtualCursor;

    private bool connected = false;

    public static InputManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
        virtualCursorUI.SetActive(false);
        InputSystem.onDeviceChange += Test;
    }

    private void Update() 
    {
        if (virtualCursor.activeInHierarchy)
        {
            var position = virtualCursor.transform.position;
            position.x = Mathf.Clamp(position.x, 0, Screen.width);
            position.y = Mathf.Clamp(position.y, 0, Screen.height);

            virtualCursor.transform.position = position;

        }
    }

    private void Test(InputDevice arg1, InputDeviceChange arg2)
    {
        foreach (string gamepad in Input.GetJoystickNames())
        {
            if (!string.IsNullOrEmpty(gamepad))
            {
                Debug.Log("Gamepad: " + gamepad + " connected.");
                ChangeCursor(GameDevice.Gamepad);
            }
            else
            {
                Debug.Log("No gamepad connected.");
                ChangeCursor(GameDevice.KeyboardAndMouse);
            }
        }
    }

    private void ChangeCursor(GameDevice device)
    {
        Cursor.visible = device == GameDevice.KeyboardAndMouse;
        virtualCursorUI.SetActive(device == GameDevice.Gamepad);
    }
}
