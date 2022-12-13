using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle05Canvas : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public string textValue;

    // Start is called before the first frame update
    void Start()
    {
        textElement.text = textValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
