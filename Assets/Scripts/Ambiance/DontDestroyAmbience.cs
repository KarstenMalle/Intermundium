using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAmbience : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource speaker;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
        DontDestroyOnLoad(speaker);
    }

    // Update is called once per frame
    void Update()
    {
        if (!speaker.isPlaying)
        {
            speaker.clip = clips[Random.Range(0, clips.Length)];
            speaker.Play();
        }
    }
}
