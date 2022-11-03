using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matchbox : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Using Matchbox");
        return true;
    }
}
