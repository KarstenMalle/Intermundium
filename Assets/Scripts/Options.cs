using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;

    [Header("Sliders")]
    public Slider SensitivitySlider;
    public static float SensitivityValue;

    public Slider VolumeSlider;
    public static float VolumeValue;

    [Header("Toggles")]
    public Toggle ToggleVSync;
    public Toggle ToggleFullscreen;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SensitivityValue = SensitivitySlider.value;
        VolumeValue = VolumeSlider.value;
    }

    public void ResMax ()
    {
        selectedResolution = 0;
    }

    public void ResMid ()
    {
        selectedResolution = 1;
    }

    public void ResLow ()
    {
        selectedResolution = 2;
    }

    public void ApplyGraphics ()
    {
        Debug.Log("Setting res to " + resolutions[selectedResolution].horizontal.ToString() + "x" + resolutions[selectedResolution].vertical.ToString());
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, Screen.fullScreen);
    }

    public void VSyncUpdate ()
    {
        if (ToggleVSync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void FullscreenUpdate()
    {
        if (ToggleFullscreen.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreen = ToggleFullscreen.isOn;
        } 
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = ToggleFullscreen.isOn;
        }
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}