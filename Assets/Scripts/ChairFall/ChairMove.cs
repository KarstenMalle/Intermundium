
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMove : MonoBehaviour
{
    [SerializeField] private string _promt;

    public string InteractionPrompt => _promt;

    [SerializeField] private Animator rightChair;
    [SerializeField] private Animator leftChair;

    [SerializeField] private Animator middleChair1;
    [SerializeField] private Animator middleChair2;

    public GameObject boxObject;
    public GameObject lightFlicker;
    public GameObject spotLight;

    public Light[] lights1;
    public Light[] lights2;

    public AudioSource[] chairs;
    public AudioSource spookSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "First Person Controller")
        {
            if(gameObject.name == boxObject.name)
            {
                StartCoroutine(firstAnimationSet());
            }
        }
    }

    private void chairAudio()
    {
        foreach(AudioSource x in chairs)
        {
            x.Play();
        }
    }

    IEnumerator firstAnimationSet()
    {
        foreach (Light x in lights1)
        {
            x.intensity = 0;
        }
        foreach (Light x in lights2)
        {
            x.intensity = 0;
        }
        //spotLight.intensity = 0;
        spotLight.SetActive(false);
        lightFlicker.SetActive(true);

        Debug.Log("State 1");
        spookSound.Play();
        rightChair.Play("ChairMove", 0, 0.0f);
        leftChair.Play("ChairUp", 0, 0.0f);
        middleChair1.Play("ChairUp", 0, 0.0f);
        middleChair2.Play("ChairUp", 0, 0.0f);
        
        yield return new WaitForSeconds(1);

        Debug.Log("State 2");
        rightChair.Play("ChairAir", 0, 0.0f);
        leftChair.Play("ChairAir", 0, 0.0f);
        middleChair1.Play("ChairAir", 0, 0.0f);
        middleChair2.Play("ChairAir", 0, 0.0f);
        yield return new WaitForSeconds(1);

        Debug.Log("State 3");
        rightChair.Play("ChairAir", 0, 0.0f);
        leftChair.Play("ChairAir", 0, 0.0f);
        middleChair1.Play("ChairAir", 0, 0.0f);
        middleChair2.Play("ChairAir", 0, 0.0f);
        yield return new WaitForSeconds(1);

        Debug.Log("State 4");
        rightChair.Play("ChairDrop", 0, 0.0f);
        leftChair.Play("ChairDown", 0, 0.0f);
        middleChair1.Play("ChairDown", 0, 0.0f);
        middleChair2.Play("ChairDown", 0, 0.0f);
        yield return new WaitForSeconds(0.7f);

        Debug.Log("State 5");
        chairAudio();
        yield return new WaitForSeconds(0.3f);
        rightChair.Play("ChairBack", 0, 0.0f);
        leftChair.Play("ChairBack", 0, 0.0f);
        middleChair1.Play("ChairBack", 0, 0.0f);
        middleChair2.Play("ChairBack", 0, 0.0f);

        yield return new WaitForSeconds(1);
        spookSound.Stop();

        lightFlicker.SetActive(false);
        //yield return new WaitForSeconds(1);
        foreach (Light x in lights1)
        {
            x.intensity = 1;
        }
        foreach (Light x in lights2)
        {
            x.intensity = 1;
        }
        //spotLight.intensity = 1;
        spotLight.SetActive(true);
        boxObject.SetActive(false);
    }

    

}

