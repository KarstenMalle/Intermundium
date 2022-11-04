using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    public string _password = "1234";
    private string _userInput = "";

    public string InteractionPrompt => _promt;

    private void Start()
    {
        _userInput = "";
    }

    public void PasswordEntered(string number)
    {
        _userInput += number;
        if (_userInput.Length == 4)
        {
            if(_userInput == _password)
            {
                Debug.Log("Correct code!");
                _userInput = "";
            }
            else
            {
                Debug.Log("Wrong code!");
                _userInput = "";
            }
        }

    }

    public bool Interact(Interactor interactor)
    {


        Start();
        
        PasswordEntered();


        return true;
    }
}
