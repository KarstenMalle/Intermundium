using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuLoad : MonoBehaviour
{
    public GameObject PauseScreen;
    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        PauseScreen.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            isPressed = !isPressed;
            if(isPressed)
            {
                PauseScreen.SetActive(true);
            }
            else
            {
                PauseScreen.SetActive(false);
            }

        }



    }
}
