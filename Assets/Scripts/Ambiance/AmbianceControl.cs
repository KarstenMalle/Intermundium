using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceControl : MonoBehaviour
{
    private AudioSource ambianceSound;

    private float time;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        ambianceSound = GetComponent<AudioSource>();
        time = Time.time;
        speed = ambianceSound.volume / 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > 5 && ambianceSound.volume > speed) {
            ambianceSound.volume -= speed * Time.deltaTime;
        }
    }
}
