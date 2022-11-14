
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;

    public string InteractionPrompt => _promt;

    [SerializeField] private Animator myDoor = null;

    private bool doorOpen = false;
    private string doorName = "";
    private float deltaT = 0;

    private GameObject player;
    public GameObject doorObject;

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

    void Update()
    {
        //Vector3 doorRelative = doorObject.transform.InverseTransformPoint(player.transform.position);

        //Debug.Log(Vector3.Distance(doorRelative, player.transform.position));

        if (doorOpen)
        {
            deltaT += Time.deltaTime;
            if (deltaT > 20)
            {

                Debug.Log("insert SLAM sound :)");
                deltaT = 0;
                if (doorName == "DoorOpen")
                {
                    myDoor.Play("DoorClose", 0, 0.0f);
                }
                else
                {
                    myDoor.Play("DoorClose2", 0, 0.0f);
                }
                doorOpen = false;
            }
        }
    }

    public bool Interact(Interactor interactor)
    {
        Vector3 doorRelative = doorObject.transform.InverseTransformPoint(player.transform.position);
        if (!doorOpen)
        {
            if (doorRelative.z > 0)
            {
                Debug.Log("Door clicked infront");
                myDoor.Play("DoorOpen2", 0, 0.0f);
                doorOpen = true;
                doorName = "DoorOpen2";

                return true;

            }
            if (doorRelative.z <= 0)
            {
                Debug.Log("Door clicked behind");
                myDoor.Play("DoorOpen", 0, 0.0f);
                doorOpen = true;
                doorName = "DoorOpen";

                return true;
            }
            else
            {
                Debug.Log("ERROR");
                return true;
            }
        }
        else
        {
            if (doorName == "DoorOpen")
            {
                myDoor.Play("DoorClose", 0, 0.0f);
            }
            else
            {
                myDoor.Play("DoorClose2", 0, 0.0f);
            }
            doorOpen = false;

            return true;
        }
    }
}

