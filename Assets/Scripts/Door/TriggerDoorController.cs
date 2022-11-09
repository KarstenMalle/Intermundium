using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;

    public string InteractionPrompt => _promt;

    [SerializeField] private Animator myDoor = null;

    private bool doorOpen = false;
    private float deltaT = 0;

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

    void Update()
    {
        if (doorOpen)
        {
            deltaT += Time.deltaTime;
            if (deltaT > 20)
            {
                Debug.Log("insert SLAM sound :)");
                deltaT = 0;
                myDoor.Play("DoorClose", 0, 0.0f);
                doorOpen = false;
            }
        }
    }

    public bool Interact(Interactor interactor)
    {
        if(!doorOpen)
        {
            Debug.Log("Door clicked");
            myDoor.Play("DoorOpen", 0, 0.0f);
            doorOpen = true;


            return true;
        }
        else
        {
            myDoor.Play("DoorClose", 0, 0.0f);
            doorOpen = false;

            return true;
        }
    }
}