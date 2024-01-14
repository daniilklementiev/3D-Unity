using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LabyMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider effectsVolumeSlider;
    [SerializeField]
    private Toggle muteAllToggle;
    [SerializeField]
    private Toggle godModeToggle;
    [SerializeField]
    private AudioMixer soundMixer;
    [SerializeField]
    private TMPro.TMP_Dropdown qualityDropdown;

    void Start()
    {
		string[] names = QualitySettings.names;
		if(names.Length == qualityDropdown.options.Count)
        {
            OnQualityChanged(qualityDropdown.value);
        }
        else
        {
            qualityDropdown.options.Clear();
            for(int i = 0; i < names.Length; i++)
            {
                qualityDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(names[i]));
            }
            qualityDropdown.value = QualitySettings.GetQualityLevel();
        }
		OnMusicVolumeSlider(musicVolumeSlider.value);
        OnEffectsVolumeSlider(effectsVolumeSlider.value);
        OnMuteAllToggle(muteAllToggle.isOn);

        if(content.activeInHierarchy) ShowMenu();
        else HideMenu();
        
    }


    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(LabyState.isPaused) HideMenu(); 
            else ShowMenu();
        }
    }

    public void OnQualityChanged(int value)
    {
        QualitySettings.SetQualityLevel(value, true);
    }

    private void ShowMenu()
    {
        LabyState.isPaused = true;
        content.SetActive(LabyState.isPaused);
        Time.timeScale = 0;
    }

    private void HideMenu()
    {
		LabyState.isPaused = false;
		content.SetActive(LabyState.isPaused);
        Time.timeScale = 1;
    }

    // UI Event listener
    public void OnMusicVolumeSlider(float value)
    {
        float dB = -80f + 90f * value;
        soundMixer.SetFloat("AmbientVolume", dB);
    }

	public void OnEffectsVolumeSlider(float value)
	{
        float dB = -80f + 90f * value;
        soundMixer.SetFloat("EffectsVolume", dB);
	}

	public void OnMuteAllToggle(bool value)
	{
        // -80 - off, 0 - on
        float dB = value ? -80f : 0f;
        soundMixer.SetFloat("MasterVolume", dB);
	}

    public void OnUiButtonClick(int id)
    {
        Debug.Log(id);
        // 1 exit, 2 reset, 3 close, 0 error
        switch(id)
        {
			case 1:
				// перевіряє чи знаходиться гра у Редакторі чи ні
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
				break;
			case 2:
				ResetSettings();
				break;
			case 3:
				HideMenu();
				break;
            case 4:
                KeysCheat();
				break;
			default:
				Debug.LogError("Unknown button id: " + id);
				break;
		}
    }

    private void KeysCheat()
    {
        if (godModeToggle.isOn)
        {
            LabyState.key1Remained = 1000;
		    LabyState.key2Remained = 1000;
        }
        else
        {
            LabyState.key1Remained = 1;
            LabyState.key2Remained = 1;
        }

    }

    private void ResetSettings()
    {
        OnMusicVolumeSlider(0.5f);
		musicVolumeSlider.value = 0.5f;
		OnEffectsVolumeSlider(0.5f);
        effectsVolumeSlider.value = 0.5f;
        OnMuteAllToggle(false);
        muteAllToggle.isOn = false;
    }


}
