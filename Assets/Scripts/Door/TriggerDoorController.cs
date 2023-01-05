
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

    [SerializeField]
    public AudioSource doorOpenSound, doorCloseSound, doorClickSound;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
    }

    void Update()
    {

        if (doorOpen)
        {
            deltaT += Time.deltaTime;
            if (deltaT > 20)
            {
                doorCloseSound.Play();
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
        if (!doorOpen && myDoor.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !myDoor.IsInTransition(0))
        {
            if (doorRelative.z > 0)
            {
                //Debug.Log("Door clicked infront");
                myDoor.Play("DoorOpen2", 0, 0.0f);
                doorOpen = true;
                doorName = "DoorOpen2";

                doorOpenSound.Play();

                return true;

            }
            if (doorRelative.z <= 0)
            {
                //Debug.Log("Door clicked behind");
                myDoor.Play("DoorOpen", 0, 0.0f);
                doorOpen = true;
                doorName = "DoorOpen";

                doorOpenSound.Play();

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
            if (doorName == "DoorOpen" && myDoor.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !myDoor.IsInTransition(0))
            {
                myDoor.Play("DoorClose", 0, 0.0f);
                doorCloseSound.Play();
            }
            else if (myDoor.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !myDoor.IsInTransition(0))
            {
                myDoor.Play("DoorClose2", 0, 0.0f);
                doorCloseSound.Play();
            }
            doorOpen = false;

            return true;
        }
    }
}

