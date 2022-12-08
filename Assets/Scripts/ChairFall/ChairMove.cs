
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
    public Light spotLight;

    public Light[] lights1;
    public Light[] lights2;

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
        spotLight.intensity = 0;
        lightFlicker.SetActive(true);

        rightChair.Play("ChairMove", 0, 0.0f);
        leftChair.Play("ChairUp", 0, 0.0f);
        middleChair1.Play("ChairUp", 0, 0.0f);
        middleChair2.Play("ChairUp", 0, 0.0f);
        
        yield return new WaitForSeconds(1);

        rightChair.Play("ChairAir", 0, 0.0f);
        leftChair.Play("ChairAir", 0, 0.0f);
        middleChair1.Play("ChairAir", 0, 0.0f);
        middleChair2.Play("ChairAir", 0, 0.0f);
        yield return new WaitForSeconds(1);
        rightChair.Play("ChairAir", 0, 0.0f);
        leftChair.Play("ChairAir", 0, 0.0f);
        middleChair1.Play("ChairAir", 0, 0.0f);
        middleChair2.Play("ChairAir", 0, 0.0f);
        yield return new WaitForSeconds(1);

        rightChair.Play("ChairDrop", 0, 0.0f);
        leftChair.Play("ChairDown", 0, 0.0f);
        middleChair1.Play("ChairDown", 0, 0.0f);
        middleChair2.Play("ChairDown", 0, 0.0f);

        yield return new WaitForSeconds(1);

        rightChair.Play("ChairBack", 0, 0.0f);
        leftChair.Play("ChairBack", 0, 0.0f);
        middleChair1.Play("ChairBack", 0, 0.0f);
        middleChair2.Play("ChairBack", 0, 0.0f);

        yield return new WaitForSeconds(1);

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
        spotLight.intensity = 1;
        boxObject.SetActive(false);
    }

    

}

