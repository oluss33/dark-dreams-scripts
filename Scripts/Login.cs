using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField username;
    public InputField gameToken;

    Action<bool> callback;

    private void Start()
    {
        if (GameJolt.API.GameJoltAPI.Instance.CurrentUser != null)
        {
            ChangeScene();
        }
    }

    public void LogIn()
    {
        //  GameJolt.API.GameJoltAPI.Instance.CurrentUser.SignIn();
        if (username.text.Trim() != string.Empty && gameToken.text.Trim() != string.Empty)
        {
            var user = new GameJolt.API.Objects.User(username.text.Trim(), gameToken.text.Trim());
            user.SignIn((bool success) =>
            {
                if (success)
                {
                    Dismiss(true);
                }
            });
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Dismiss(bool success)
    {
        if(callback != null)
        {
            callback(success);
            callback = null;
        }
    }

}
