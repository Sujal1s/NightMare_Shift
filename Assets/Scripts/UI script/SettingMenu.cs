using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropDown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] customResolutions = new Resolution[]
    {
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 2560, height = 1440 }
    };

    private int currentResolutionIndex;
    private bool isInitialized = false;
    private string previousScene;

    void Start()
    {
        previousScene = PlayerPrefs.GetString("PreviousScene", "MainMenu"); // Load previous scene

        // Initialize resolution settings
        resolutionDropDown.ClearOptions();
        List<string> resolutionOptions = new List<string>();

        if (!PlayerPrefs.HasKey("ResolutionIndex"))
        {
            for (int i = 0; i < customResolutions.Length; i++)
            {
                if (customResolutions[i].width == Screen.currentResolution.width &&
                    customResolutions[i].height == Screen.currentResolution.height)
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

        // Initialize fullscreen setting
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Initialize graphics quality settings
        qualityDropdown.ClearOptions();
        List<string> qualityLevels = new List<string>(QualitySettings.names);
        qualityDropdown.AddOptions(qualityLevels);

        int savedQuality = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.GetQualityLevel());
        qualityDropdown.value = savedQuality;
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // Apply saved settings
        SetResolution(currentResolutionIndex);
        SetQuality(savedQuality);

        isInitialized = true;
    }

    void Update()
    {
        // Detect "B" button (Gamepad Button 1) or "Escape" key (Keyboard) to go back
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Fire2"))
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
            Debug.Log("Resolution Set: " + resolution.width + "x" + resolution.height);
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
        PlayerPrefs.Save();

        Debug.Log("Graphics Quality Set: " + QualitySettings.names[qualityIndex]);

        ApplyGraphicsSettings(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Fullscreen Set: " + isFullscreen);
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
        Debug.Log("Returning to: " + previousScene);
        SceneManager.LoadScene(previousScene);
    }

    private void ApplyGraphicsSettings(int qualityIndex)
    {
        if (qualityIndex == 0) // "Very Low" setting
        {
            Debug.Log("Applying VERY LOW graphics settings...");

            // Disable all shadows
            QualitySettings.shadows = ShadowQuality.Disable;

            // Reduce texture quality to minimum
            QualitySettings.globalTextureMipmapLimit = 2;

            // Disable anti-aliasing
            QualitySettings.antiAliasing = 0;

            // Reduce render scale (makes everything pixelated)
            ScalableBufferManager.ResizeBuffers(0.5f, 0.5f);

            // Disable reflection probes & baked lighting
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.softParticles = false;

            // Disable all post-processing effects
            foreach (var cam in Camera.allCameras)
            {
                if (cam.GetComponent<UnityEngine.Rendering.Volume>())
                {
                    cam.GetComponent<UnityEngine.Rendering.Volume>().enabled = false;
                }
            }

            // Force sprite resolution to lowest possible
            foreach (var renderer in FindObjectsOfType<SpriteRenderer>())
            {
                renderer.sprite.texture.filterMode = FilterMode.Point;
            }
        }
        else
        {
            // Restore default settings for higher graphics settings
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.globalTextureMipmapLimit = 0;
            QualitySettings.antiAliasing = 2;
            ScalableBufferManager.ResizeBuffers(1f, 1f);
            QualitySettings.realtimeReflectionProbes = true;
            QualitySettings.softParticles = true;

            foreach (var cam in Camera.allCameras)
            {
                if (cam.GetComponent<UnityEngine.Rendering.Volume>())
                {
                    cam.GetComponent<UnityEngine.Rendering.Volume>().enabled = true;
                }
            }

            foreach (var renderer in FindObjectsOfType<SpriteRenderer>())
            {
                renderer.sprite.texture.filterMode = FilterMode.Bilinear;
            }
        }
    }
}
