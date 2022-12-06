using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    [SerializeField] private Animator myToilet = null;

    private bool isOpen;
    private GameObject player;
    public GameObject doorObject;
    private bool _ePressed = false;
    public GameObject toolTip;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        isOpen = false;
        toolTip.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, doorObject.transform.position) < (2 * 1) && _ePressed == false)
        {
            toolTip.SetActive(true);
        }
        else
        {
            toolTip.SetActive(false);
        }
    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        _ePressed = true;
        Debug.Log("TOILET");
        if (!isOpen)
        {
            myToilet.Play("DoorOpen", 0, 0.0f);
            isOpen = true;

            return true;
        }

        if (isOpen)
        {
            myToilet.Play("DoorClose", 0, 0.0f);
            isOpen = false;

            return true;
        }
        return true;

    }
}




