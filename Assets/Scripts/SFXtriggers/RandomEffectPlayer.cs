using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomEffectPlayer : MonoBehaviour
{
    public AudioSource[] speakers;
    private int index = 0;
    private int prev_index = 0;

    private float time;

    [Tooltip("Initial wait time in seconds")]
    public float wait = 60f;

    public int max;
    public int min;

    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // rnd.Next(min, max+1) returns a random number between min and max
        if (Time.time - time > rnd.Next(min, max+1))
        {
            // rnd.Next(0, speakers.Length) returns a random number between 0 and speakers.Length
            while (index == prev_index) {
                index = rnd.Next(0, speakers.Length);
            }

            Debug.Log("Playing from speaker" + (index+1));
            speakers[index].Play();
            time = Time.time;
            wait = rnd.Next(min, max+1);
            Debug.Log("Wait: " + wait);
            prev_index = index;
        }
    }
}
