using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Puzzle05 : MonoBehaviour, IInteractable
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

    public GameObject wallDeco;

    [SerializeField] private GameObject canvas;

    private string correctAnswer = "1087";

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
                    if(userGuess == correctAnswer)
                    {
                        Debug.Log("CORRECT, UNLOCK NEXT LEVEL");
                        nextLvlBlocker.SetActive(false);
                        Time.timeScale = 1f;
                        Cursor.lockState = CursorLockMode.Locked;
                        FirstPersonLook.sensitivity = tmp_sensitivity;
                        _ePressed = false;
                        canvas.SetActive(false);
                        overlay_activation.entering_code = true;
                        wallDeco.SetActive(false);
                        overlay_activation.have_pages = false;
                    }
                    else if (userGuess != correctAnswer)
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
            overlay_activation.entering_code = false;
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
        overlay_activation.entering_code = true;
        _ePressed = true;

        Debug.Log("U pressed the page5");
        

        return true;
    }
}
