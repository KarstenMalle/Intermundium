using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpook : MonoBehaviour
{
    public float time_till_spook;

    private float time_at_start;

    private bool spooked = false;

    AudioSource playSound;

    // Start is called before the first frame update
    void Start()
    {
        playSound = GetComponent<AudioSource>();
        time_at_start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spooked) {
            if (Time.time - time_at_start >= time_till_spook) {
                spooked = true;
                playSound.Play();
            }
        }
    }
}
