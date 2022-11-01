using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AAText : MonoBehaviour
{
    public TextMeshProUGUI AntiAliasingText;

    void Start()
    {
        AntiAliasingText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }

    public void aaTextUpdate()
    {
        AntiAliasingText.text = QualitySettings.antiAliasing.ToString() + "x";
    }
}
