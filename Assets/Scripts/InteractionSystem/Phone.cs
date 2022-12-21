using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
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
    public GameObject toolTip;
    private GameObject player;
    public GameObject phoneObject;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        toolTip.SetActive(false);
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
