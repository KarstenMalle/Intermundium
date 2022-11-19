
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosed : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;

    public string InteractionPrompt => _promt;

    private GameObject player;
    public GameObject doorObject;

    [SerializeField]
    public AudioSource doorClickSound;

    //[SerializeField] private bool openTrigger = false;

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                Debug.Log("DAMN");
                myDoor.Play("OpenDoor", 0, 0.0f);
                gameObject.SetActive(false);
                Debug.Log("YEET");
            }
        }
    }
    */
    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
    }

    public bool Interact(Interactor interactor)
    {
        doorClickSound.Play();

        return true;
    }

    
}

