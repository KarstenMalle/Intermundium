using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CassettePlayerNew : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;

    public GameObject toolTip;
    private GameObject player;
    public GameObject cassetteObject;
    public GameObject bookObject;
    public GameObject bathtubObject;
    public GameObject cassette4Object;

    [SerializeField] private IntSO cassettesSO;
    private bool cassettesReady = false;
    public AudioSource cassette1Sound, cassette2Sound, cassette3Sound, cassette4Sound;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        toolTip.SetActive(false);
        bathtubObject.SetActive(false);
        bookObject.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, cassetteObject.transform.position) < (2 * 1) && _ePressed == false)
        {
            //Debug.Log((player.transform.position - this.transform.position).sqrMagnitude);
            toolTip.SetActive(true);
        }
        else
        {
            toolTip.SetActive(false);
        }
        if (_ePressed && cassettesReady)
        {
            if (cassettesSO.Value == 0)
            {
                Debug.Log("No cassette");
                cassettesReady = false;
            }
            else if (cassettesSO.Value == 1)
            {
                Debug.Log("Play first cassette");
                cassette1Sound.Play();
                cassettesReady = false;
            }
            else if (cassettesSO.Value == 2)
            {
                Debug.Log("Play second cassette");
                cassette2Sound.Play();
                bathtubObject.SetActive(true);
                cassettesReady = false;
            }
            else if (cassettesSO.Value == 3)
            {
                Debug.Log("Play third cassette");
                bathtubObject.SetActive(false);
                cassette3Sound.Play();
                cassettesReady = false;
                
            }
            else if (cassettesSO.Value == 4)
            {
                Debug.Log("Play fourth cassette");
                bookObject.SetActive(true);
                cassette4Sound.Play();
                cassettesReady = false;
            }
            else
            {
                Debug.Log("ERROR");
                cassettesReady = false;
            }

        }
    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        _ePressed = true;
        cassettesReady = true;
        return true;
    }
}