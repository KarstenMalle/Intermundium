using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTriggerSFX : MonoBehaviour
{
    public AudioSource playSound;

    int count = 0;

    void OnTriggerEnter(Collider other) {
        if (count == 0) {
            playSound.Play();
        }
        count++;
    }
}

/* USAGE GUIDE
 * 1. Create cube that acts as a speaker and attach audio source to it
 * 2. Make sure audio source has spatial blend = 1
 * 3. Create cube that acts as a trigger
 * 4. Attach this script to trigger cube
 * 5. Drag the audio source cube from the hierarchy to "Play Sound"
 */