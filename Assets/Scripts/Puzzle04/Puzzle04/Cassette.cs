using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassette : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed;

    static public bool cassetteCollected;
    [SerializeField] private IntSO cassettesSO;
    public GameObject cassetteObject;


    private float deltaT = 0;
    // Start is called before the first frame update
    void Start()
    {
        _ePressed = false;
    }

    // Update is called once per frame
    void Update()
    { 

        if (_ePressed)
        {
            cassettesSO.Value += 1;
            cassetteObject.SetActive(false);
            _ePressed = false;

        }

    }

    public string InteractionPrompt => _promt;
    public bool Interact(Interactor interactor)
    {
        _ePressed = true;
        Debug.Log("Clicked: " + _ePressed);
        return true;
    }
}