using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropDown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] customResolutions = new Resolution[]
    {
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1440, height = 1080 },
        new Resolution { width = 1920, height = 1080 }
    };

    private int currentResolutionIndex;
    private bool isInitialized = false;

    void Start()
    {
        resolutionDropDown.ClearOptions();
        List<string> resolutionOptions = new List<string>();

        if (!PlayerPrefs.HasKey("ResolutionIndex"))
        {
            for (int i = 0; i < customResolutions.Length; i++)
            {
                if (customResolutions[i].width == Screen.currentResolution.width && customResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                    break;
                }
            }
            PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
        }
        else
        {
            currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
        }

        foreach (var res in customResolutions)
        {
            resolutionOptions.Add(res.width + " x " + res.height);
        }

        resolutionDropDown.AddOptions(resolutionOptions);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
        resolutionDropDown.onValueChanged.AddListener(SetResolution);

        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        qualityDropdown.ClearOptions();
        List<string> qualityLevels = new List<string>(QualitySettings.names);
        qualityDropdown.AddOptions(qualityLevels);

        int savedQuality = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.GetQualityLevel());
        qualityDropdown.value = savedQuality;
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        SetResolution(currentResolutionIndex);
        SetQuality(savedQuality);

        isInitialized = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Back();
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        if (!isInitialized) return;

        if (resolutionIndex >= 0 && resolutionIndex < customResolutions.Length)
        {
            Resolution resolution = customResolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("volume", volume);
        }
    }

    public void Back()
    {
        string lastScene = PlayerPrefs.GetString("PreviousScene", "MainMenu");

        // Load the previous scene first in additive mode
        if (lastScene != "SettingMenu")
        {
            SceneManager.LoadScene(lastScene, LoadSceneMode.Additive);
        }

        // Unload SettingMenu after loading the previous scene
        SceneManager.UnloadSceneAsync("SettingMenu");
    }
}
