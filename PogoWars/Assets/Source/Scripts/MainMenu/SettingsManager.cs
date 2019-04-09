using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    [Header("Reference")]
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Slider musicVolumeSlider;
    public Button applyButton;

    public AudioSource musicSource;
    public Resolution[] resolutions;
    private GameSettings gameSettings;

    void OnEnable() {

        //added to fix a bug, checks to see if there is an existing file, and if there isn't creates one by calling SaveSettings
        if (!File.Exists(Application.persistentDataPath + "/gamesettings.json"))
            SaveSettings();

        //intialize a new instance
        gameSettings = new GameSettings();

        //on the event that a the value is changed point to the relevant function.
        fullscreenToggle.onValueChanged.AddListener(delegate{ OnFullscreenToggle(); } );
        resolutionDropdown.onValueChanged.AddListener(delegate{ OnResolutionChange(); } );
        musicVolumeSlider.onValueChanged.AddListener(delegate{ OnVolumeChange(); } );
        applyButton.onClick.AddListener(delegate { OnApplyButton();});

        //apply the list of resolutions supported 
        resolutions = Screen.resolutions;
        //gets the array of resolutions and adds them to the dropdown menu
        foreach( Resolution resolution in resolutions)
        {
            //Adds one at a time
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString())); 
        }

        LoadSettings();
    }

    public void OnFullscreenToggle()
    {
        //toggles fullscreen by accessing Screen
         gameSettings.fullScreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        //Assigns the resolution based on the index from the array
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnVolumeChange()
    {
        //didn't get round to adding music to the game but the functionality for changing the volume is roughly there but untested.
        musicSource.volume = gameSettings.masterVolume = musicVolumeSlider.value;
    }

    public void OnApplyButton()
    {
        //call the function when clicking the apply button
        SaveSettings();
    }

    public void SaveSettings()
    {
        //applys the values to a Json File located at the designated path in the %appdata% folder
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        //pass in data from location path
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        //apply the settings to each of the setting options
        musicVolumeSlider.value = gameSettings.masterVolume;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullScreen;
        //makes sure that the value on the dropdown is the same as the one in the file
        resolutionDropdown.RefreshShownValue();
    }
}
