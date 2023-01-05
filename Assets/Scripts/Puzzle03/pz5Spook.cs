using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pz5Spook : MonoBehaviour
{

    private AudioSource source;

    private bool spooped = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (overlay_activation.have_pages && !spooped) {
            source.Play();
            spooped = true;
        }
    }
}
