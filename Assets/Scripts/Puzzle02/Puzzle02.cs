using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Puzzle02 : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;
    private bool _eReleased = false;

    public GameObject toolTip;
    private GameObject player;
    public GameObject pageObject;
    public TMP_InputField userInputField;
    public TextMeshProUGUI placeholder;
    public GameObject nextLvlBlocker;

    private string userGuess = "";
    private float tmp_sensitivity;
    //public InputField userInputField;

    [SerializeField] private GameObject canvas;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        toolTip.SetActive(false);
        canvas.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && _ePressed)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            FirstPersonLook.sensitivity = tmp_sensitivity;
            _ePressed = false;
        
        }
        if (Input.anyKeyDown && _ePressed)
        {
            if (_eReleased)
            {
                placeholder.gameObject.SetActive(false);
                if(!Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.Backspace))
                {
                    userGuess += Input.inputString;
                    userInputField.text = userGuess;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    string upperCase = userGuess.ToUpper();
                    userGuess = upperCase;
                    if(userGuess == "LIMBO")
                    {
                        Debug.Log("CORRECT, UNLOCK NEXT LEVEL");
                        nextLvlBlocker.SetActive(false);
                        Time.timeScale = 1f;
                        Cursor.lockState = CursorLockMode.Locked;
                        FirstPersonLook.sensitivity = tmp_sensitivity;
                        _ePressed = false;
                        canvas.SetActive(false);
                    }
                    else if (userGuess != "LIMBO")
                    {
                        Debug.Log("WRONG");
                    }
                    userGuess = "";
                    userInputField.text = "";
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                { 
                    string guessMinus1 = userGuess.Substring(0, userGuess.Length-1);
                    Debug.Log(guessMinus1);
                    userGuess = guessMinus1;
                    userInputField.text = guessMinus1;
                }
            }
            _eReleased = true;
        }
    
        
        if (Vector3.Distance(player.transform.position, pageObject.transform.position) < (2 * 1) && _ePressed == false)
        {
            //Debug.Log((player.transform.position - this.transform.position).sqrMagnitude);
            toolTip.SetActive(true);
        }
        else
        {
            toolTip.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            canvas.SetActive(false);
        }
    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        if(!_ePressed)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            tmp_sensitivity = FirstPersonLook.sensitivity;
            FirstPersonLook.sensitivity = 0f;
        }
        canvas.SetActive(true);
        _ePressed = true;
        
        //Update();

        Debug.Log("U pressed the page2");
        

        return true;
    }
}
