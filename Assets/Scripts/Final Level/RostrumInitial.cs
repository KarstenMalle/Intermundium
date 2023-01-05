using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RostrumInitial : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;

    [SerializeField] private GameObject initialObject;
    [SerializeField] private GameObject newObject;

    // Start is called before the first frame update
    void Start()
    {
        initialObject.SetActive(true);
        newObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        _ePressed = true;
        newObject.SetActive(true);
        initialObject.SetActive(false);
        //Update();

        Debug.Log("U pressed the page2");


        return true;
    }
}
