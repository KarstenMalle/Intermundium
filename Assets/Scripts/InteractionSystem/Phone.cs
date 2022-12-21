using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Phone : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;
    private bool _eReleased = false;


    public int _password = 8527;
    int _guessedNumber;
    private string _guessedDigits = "";

    public GameObject levelBlocker;
    public GameObject toolTip;
    private GameObject player;
    public GameObject phoneObject;

    
    public TMP_InputField userInputField;
    public TextMeshProUGUI placeholder;
    [SerializeField] private GameObject canvas;
    private float tmp_sensitivity;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        toolTip.SetActive(false);
        canvas.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, phoneObject.transform.position) < (2 * 1) && _ePressed == false)
        {
            //Debug.Log((player.transform.position - this.transform.position).sqrMagnitude);
            toolTip.SetActive(true);
        }
        else
        {
            toolTip.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q) && _ePressed)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            FirstPersonLook.sensitivity = tmp_sensitivity;
            _ePressed = false;
            canvas.SetActive(false);
        }
        if (Input.anyKeyDown && _ePressed)
        {
            placeholder.gameObject.SetActive(false);

            if (int.TryParse(Input.inputString, out _guessedNumber))
            {
                if (!Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.Backspace))
                {
                    _guessedDigits += _guessedNumber.ToString();
                    userInputField.text = _guessedDigits;
                }
                if (Input.GetKeyDown(KeyCode.Return) || _guessedDigits.Length == 4)
                {
                    if (int.Parse(_guessedDigits) == _password)
                    {
                        Debug.Log("Correct code!");
                        levelBlocker.SetActive(false);
                        _guessedDigits = "";
                        userInputField.text = "";
                        Time.timeScale = 1f;
                        Cursor.lockState = CursorLockMode.Locked;
                        FirstPersonLook.sensitivity = tmp_sensitivity;
                        _ePressed = false;
                        canvas.SetActive(false);

                    }
                    else
                    {
                        Debug.Log("Wrong code!");
                        _guessedDigits = "";
                        userInputField.text = "";
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    string guessMinus1 = _guessedDigits.Substring(0, _guessedDigits.Length - 1);
                    _guessedDigits = guessMinus1;
                    userInputField.text = guessMinus1;
                }
            }
        }

    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        if (!_ePressed)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            tmp_sensitivity = FirstPersonLook.sensitivity;
            FirstPersonLook.sensitivity = 0f;
            canvas.SetActive(true);
        }
        Debug.Log("Using Phone");
        _ePressed = true;


        return true;
    }
}


