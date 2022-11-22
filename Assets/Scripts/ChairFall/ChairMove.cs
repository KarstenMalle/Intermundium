
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
    public GameObject box2Object;

    public Light[] lights1;
    public Light[] lights2;

    private bool isTriggeredBox1 = false;
    private bool isTriggeredBox2 = false;

    IEnumerator sendMessage(string message, Func<bool> onSuccess)
    {
        yield return new WaitForSeconds(1);
        //.........
        //Send message algorithm
        onSuccess(result);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "First Person Controller")
        {
            if(gameObject.name == boxObject.name && isTriggeredBox1 != true)
            {

                isTriggeredBox1 = true;
                StartCoroutine(firstAnimationSet());
                

            }
            if (gameObject.name == box2Object.name && isTriggeredBox2 != true)
            {
                isTriggeredBox2 = true;
                Debug.Log("WHAT THAT VALUE DO: " + isTriggeredBox2);
                StopCoroutine(firstAnimationSet());
                StartCoroutine(secondAnimationSet());

            }



        }
    }

    IEnumerator firstAnimationSet()
    {
        rightChair.Play("ChairMove", 0, 0.0f);
        leftChair.Play("ChairUp", 0, 0.0f);
        middleChair1.Play("ChairUp", 0, 0.0f);
        middleChair2.Play("ChairUp", 0, 0.0f);
        for (int i = 0; i < 5; i++)
        {
            foreach (Light x in lights1)
            {
                x.intensity = 0;
            }
            foreach (Light x in lights2)
            {
                x.intensity = 2;
            }
            yield return new WaitForSeconds((float)0.2);
            foreach (Light x in lights1)
            {
                x.intensity = 2;
            }
            foreach (Light x in lights2)
            {
                x.intensity = 0;
            }
            yield return new WaitForSeconds((float)0.2);
        }

        //yield return new WaitForSeconds(2);

        rightChair.Play("ChairAir", 0, 0.0f);
        leftChair.Play("ChairAir", 0, 0.0f);
        middleChair1.Play("ChairAir", 0, 0.0f);
        middleChair2.Play("ChairAir", 0, 0.0f);

        bool loopWhile = true;
        bool tempBool = false;
        while(loopWhile)
        {
            Debug.Log(StartCoroutine(boolCheck()));
            Debug.Log("WORK???: ");
            foreach (Light x in lights1)
            {
                x.intensity = 2;
            }
            foreach (Light x in lights2)
            {
                x.intensity = 0;
            }
            yield return new WaitForSeconds((float)0.2);
            foreach (Light x in lights1)
            {
                x.intensity = 0;
            }
            foreach (Light x in lights2)
            {
                x.intensity = 2;
            }
            yield return new WaitForSeconds((float)0.2);
            if(tempBool == true)
            {
                Debug.Log("HELLO???: ");
                boxObject.SetActive(false);
                loopWhile = false;
                break;
            }
        }
        
    }

    IEnumerator boolCheck()
    {
        yield return isTriggeredBox2;
    }

    IEnumerator secondAnimationSet()
    {
        rightChair.Play("ChairDrop", 0, 0.0f);
        leftChair.Play("ChairDown", 0, 0.0f);
        middleChair1.Play("ChairDown", 0, 0.0f);
        middleChair2.Play("ChairDown", 0, 0.0f);

        for (int i = 0; i < 2; i++)
        {
            foreach (Light x in lights1)
            {
                x.intensity = 0;
            }
            foreach (Light x in lights2)
            {
                x.intensity = 2;
            }
            yield return new WaitForSeconds((float)0.25);
            foreach (Light x in lights1)
            {
                x.intensity = 2;
            }
            foreach (Light x in lights2)
            {
                x.intensity = 0;
            }
            yield return new WaitForSeconds((float)0.25);
        }

        yield return new WaitForSeconds(1);

        rightChair.Play("ChairBack", 0, 0.0f);
        leftChair.Play("ChairBack", 0, 0.0f);
        middleChair1.Play("ChairBack", 0, 0.0f);
        middleChair2.Play("ChairBack", 0, 0.0f);
        box2Object.SetActive(false);

        foreach (Light x in lights1)
        {
            x.intensity = 1;
        }
        foreach (Light x in lights2)
        {
            x.intensity = 1;
        }

    }

}

