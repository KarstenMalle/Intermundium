using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Options : MonoBehaviour
{

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;

    [Header("Sliders")]
    public Slider SensitivitySlider;
    public static float SensitivityValue;

    public Slider VolumeSlider;
    public static float VolumeValue;

    public Slider AntiAliasingSlider;
    public static int AntiAliasingValue;
    private int AAtmp;

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

    public void AntiAliasingUpdate()
    {
        decimal tmp = new decimal(AntiAliasingSlider.value);
        AAtmp = (int)Math.Floor(tmp);
        AntiAliasingValue = UpdateAA(AAtmp);
        QualitySettings.antiAliasing = AntiAliasingValue;
    }

    int UpdateAA(int x)
    {
        if (x == 0)
        {
            return 0;
        } 
        else if (x == 1)
        {
            return 2;
        }
        else if (x == 2)
        {
            return 4;
        }
        else if (x == 3)
        {
            return 8;
        }
        else
        {
            return 0;
        }
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}