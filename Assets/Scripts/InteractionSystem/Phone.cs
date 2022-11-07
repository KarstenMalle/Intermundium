using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    public int _password = 1234;
    int _guessedNumber;
    private string _guessedDigits = "";
    private bool _ePressed = false;

    public GameObject levelBlocker;

    void Update()
    {
        if (Input.anyKeyDown && _ePressed)
        {
            if (int.TryParse(Input.inputString, out _guessedNumber))
            {
                _guessedDigits += _guessedNumber.ToString();
       
                Debug.Log(_guessedNumber);
                Debug.Log(_guessedDigits);
                if (int.Parse(_guessedDigits) == _password)
                {
                    Debug.Log("Correct code!");
                    levelBlocker.SetActive(false);
                }
                else
                {
                    Debug.Log("Wrong code!");
                }

                if (_guessedDigits.Length == 4)
                {
                    _guessedDigits = "";
                }
            }
        }

    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Using Phone");
        _ePressed = true;
        
        Update();
        


        return true;
    }
}
