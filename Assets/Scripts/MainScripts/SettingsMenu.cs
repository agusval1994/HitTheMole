using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    
    public GameManager gameManager;
    public UIMainMenu uiMenu;
    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown;
    public Toggle fxToggleButton;
    public Toggle musicToggleButton;

    int fx = 0; // 0=true, 1=false
    int music = 0; // 0=true, 1=false
    int graphics = 2; // 2=high, 1=medium, 0=low

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i=0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        LoadPlayerPrefs();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFx()
    {        
        if(fx == 0)
        {
            fx = 1;
        }
        else
        {
            fx = 0;
        }

        SavePlayerPrefs();

        FindObjectOfType<AudioManager>().UpdateFx();
    }

    public void SetMusic()
    {
        if (music == 0)
        {
            music = 1;
        }
        else
        {
            music = 0;
        }

        SavePlayerPrefs();

        FindObjectOfType<AudioManager>().UpdateMusic();
    }

    public void UpdateSettingsUI()
    {
        if (fx == 0)
        {
            fxToggleButton.SetIsOnWithoutNotify(true);
        }
        else
        {
            fxToggleButton.SetIsOnWithoutNotify(false);
        }

        if(music == 0)
        {
            musicToggleButton.SetIsOnWithoutNotify(true);
        }
        else
        {
            musicToggleButton.SetIsOnWithoutNotify(false);
        }

        graphicsDropdown.value = graphics;
        QualitySettings.SetQualityLevel(graphics);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        SavePlayerPrefs();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("Graphics", graphics);
        PlayerPrefs.SetInt("Fx", fx);
        PlayerPrefs.SetInt("Music", music);
    }

    public void LoadPlayerPrefs()
    {
        graphics = PlayerPrefs.GetInt("Graphics", 2);
        fx = PlayerPrefs.GetInt("Fx", 0);
        music = PlayerPrefs.GetInt("Music", 0);

        UpdateSettingsUI();
    }

    public void ResetPrefes()
    {
        fx = 0;
        music = 0;
        graphics = 2;

        SavePlayerPrefs();

        FindObjectOfType<AudioManager>().UpdateFx();
        FindObjectOfType<AudioManager>().UpdateMusic();
        LoadPlayerPrefs();
    }

    public void ResetGameData()
    {
        fx = 0;
        music = 0;
        graphics = 2;

        SavePlayerPrefs();

        gameManager.DeleteData();
        gameManager.InitialData();

        uiMenu.UpdateUI();
    }
}
