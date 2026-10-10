using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.Events;
using Scripts.Player;
using UnityEngine.Serialization;

namespace Scripts.Menu
{
    public class Options : MonoBehaviour
    {

        [Header("UI")]
        public GameObject videoPanel;
        public GameObject advancedPanel;

        [Header("Buttons")] 
        public Button backBTN;
        

        [Header("Video Settings")]
        public Dropdown resolutionDropdown;
        public Dropdown qualityDropdown;
        public Toggle vSyncToggle;

        [Header("Volume Settings")]
        public Slider effectsSlider;
        public Slider masterSlider;
        public Slider musicSlider;
        public AudioMixer effectsMixer;
        public AudioMixer musicMixer;

        const string prefName = "optionvalue";
        const string resName = "resolutionoption";
        private string DATA_PATH;
        int vSyncInt;
        private bool isVsync = true;

        [Header("Player")]
        public GameObject player;

        Resolution[] resolutions;

        void Awake()
        {
            vSyncInt = PlayerPrefs.GetInt("vsync");

            if(vSyncInt == 1) {
                isVsync = true;
                vSyncToggle.isOn = true;
            } else {
                vSyncToggle.isOn = false;
                isVsync = false;
            }


            resolutionDropdown.onValueChanged.AddListener(index =>
            {
                PlayerPrefs.SetInt(resName, resolutionDropdown.value);
                PlayerPrefs.Save();
            });
            qualityDropdown.onValueChanged.AddListener(index =>
            {
                PlayerPrefs.SetInt(prefName, qualityDropdown.value);
                PlayerPrefs.Save();
            });

        }


        void Start()
        {
            LoadBTN();

            qualityDropdown.value = PlayerPrefs.GetInt(prefName, 3);
            effectsSlider.value = PlayerPrefs.GetFloat("EffectsVol", 1f);
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);
            effectsSlider.value = PlayerPrefs.GetFloat("EffectsVol", 1f);
            masterSlider.value =  PlayerPrefs.GetFloat("MasterVol", 1f);

            videoPanel.SetActive(true);
            advancedPanel.SetActive(false);

            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].ToString();
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
            resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int index)
        {
            Resolution resolution = resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetEffectsVolume(float efVol)
        {
            effectsMixer.SetFloat("EffectsVol", efVol);
            PlayerPrefs.SetFloat("EffectVol", effectsSlider.value);
        }

        public void SetMusicVolume(float musVol)
        {
            musicMixer.SetFloat("MusicVol", musVol);
            PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        }

        public void SetMaster(float masVol)
        {
            AudioListener.volume = masVol;
            PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
        }


        public void Quality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }

        public void FullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;

            if (isFullScreen == false)
            {
                PlayerPrefs.SetInt("fullscreen", 0);
            }
            else
            {
                isFullScreen = true;
                PlayerPrefs.SetInt("fullscreen", 1);
            }
        }

        public void VSync(bool isVsync) {
            if(isVsync == true) {
                QualitySettings.vSyncCount = 1;
                PlayerPrefs.SetInt("vsync", 1);
            } else {
                QualitySettings.vSyncCount = 0;
                PlayerPrefs.SetInt("vsync", 0);
            }
        }

        public void SaveBTN()
        {
            SaveData();
        }

        public void LoadBTN()
        {
            LoadData();
        }

        void SaveData()
        {
            FileStream file = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                file = File.Create(Application.persistentDataPath + DATA_PATH);
                GameObject p = new GameObject();
                bf.Serialize(file, p);
            }
            catch (Exception e)
            {
                if (e != null)
                {

                }
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        void LoadData()
        {
            FileStream file = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                file = File.Open(Application.persistentDataPath + DATA_PATH, FileMode.Open);

                player = bf.Deserialize(file) as GameObject;
            }
            catch (Exception e)
            {
                if (e != null)
                {

                }
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        public void Video()
        {
            videoPanel.SetActive(true);
            advancedPanel.SetActive(false);
        }

        public void Advanced()
        {
            videoPanel.SetActive(false);
            advancedPanel.SetActive(true);
        }
    }
}
