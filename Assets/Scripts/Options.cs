using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;

    public Slider SensitivitySlider;
    public static float SensitivityValue;

    void Start()
    {
        SensitivityValue = SensitivitySlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        SensitivityValue = SensitivitySlider.value;
        Debug.Log(SensitivityValue);
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
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, true);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}