using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConceptManager : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject conceptArtsUI;
    public static ConceptManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    public void Back()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            conceptArtsUI.SetActive(false);
            buttons[i].SetActive(false);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
