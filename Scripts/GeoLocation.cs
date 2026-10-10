using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using Scripts.Language;

public class GeoLocation : MonoBehaviour
{
    private const string url = "http://ipinfo.io";

    [SerializeField] private Dropdown dropdown;
    
    private void Start()
    {
        FetchCountry();
    }

    void FetchCountry()
    {
        using (WebClient webClient = new WebClient())
        {
            string response = webClient.DownloadString(url);
            var json = JsonUtility.FromJson<IPInfoResponse>(response);
            Debug.Log("Country: " + json.country);

            if (json.country == "PL")
            {
                LanguageManager.instance.ChangeLang("pl");
                dropdown.value = 1;
            }
            else
            {
                LanguageManager.instance.ChangeLang("en");
                dropdown.value = 0;
            }
            
        }
    }
}

[Serializable]
class IPInfoResponse
{
    public string country;
}
