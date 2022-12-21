
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosedCopy : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    public string InteractionPrompt => _promt;

    private GameObject playerView = null;

    public GameObject monster;

    private GameObject player;
    public GameObject doorObject;

    [SerializeField]
    public AudioSource doorClickSound;

    private bool ready = false;
    private bool spooked = false;
    
    private float viewAtClick;

    
    void Start()
    {
        playerView = GameObject.FindGameObjectWithTag("MainCamera");
        monster.SetActive(false);
    }

    void Update() {
        if (ready && !spooked){
            float view = playerView.transform.eulerAngles.y;

            if ((viewAtClick - view) > 150) {
                spooked = true;
                //insert jumpscare
                MonsterScript.ready = true;
                monster.SetActive(true);
            }
        }
    }

    public bool Interact(Interactor interactor)
    {
        doorClickSound.Play();
        viewAtClick = Camera.main.transform.eulerAngles.y;

        ready = true;

        return true;
    }

    
}

