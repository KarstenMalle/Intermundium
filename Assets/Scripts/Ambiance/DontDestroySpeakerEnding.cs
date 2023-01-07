using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySpeakerEnding : MonoBehaviour
{
    private AudioSource speaker;

    private GameObject backgroundMusic;


    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
        Destroy(backgroundMusic);
        
        speaker = GetComponent<AudioSource>();
        DontDestroyOnLoad(speaker);
    }
}
