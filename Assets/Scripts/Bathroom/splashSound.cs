using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashSound : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem _parentParticleSystem;

    [SerializeField]
    public AudioSource splashAudio;

    private int prevParticleCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // if the number of particles is fewer than previous frame, then play the sound
        if (_parentParticleSystem.particleCount < prevParticleCount)
        {
            splashAudio.Play();
        }

        // update the previous particle count
        prevParticleCount = _parentParticleSystem.particleCount;
    }
}
