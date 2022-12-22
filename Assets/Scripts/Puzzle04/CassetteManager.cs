using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static InspectEngine;

public class CassetteManager : MonoBehaviour
{
    
    [SerializeField]private List<CassettePlayer> cassettePlayers;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WhatCassettePlayer();
    }

    private CassettePlayer WhatCassettePlayer()
    {
        foreach (CassettePlayer cassetteplayer in cassettePlayers) {
            if (InspectEngine.GetObjectLookingAtCurrently() == cassetteplayer.transform.name) //Get the Current object we are looking at
            {
                Debug.Log("Currently looking at Cassette player {0}", cassetteplayer);
                return cassetteplayer;
            }
            else
            {
                throw new System.Exception("Cassette Player does not exist in this context");
            }
        }
        return null; //Not possible case 
    }

    IEnumerator AudioPlayerController(CassettePlayer CurrentCassettePlay)
    {   
        
        yield return null;
    }


}
