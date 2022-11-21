using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;

public class Paintings : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;

    public GameObject toolTip;
    private GameObject player;
    public GameObject paintingObject;


    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        toolTip.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, paintingObject.transform.position) < (3 * 1) && _ePressed == false)
        {
            //Debug.Log((player.transform.position - this.transform.position).sqrMagnitude);
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
        //Update();

        Debug.Log("U pressed the page2");


        return true;
    }
}