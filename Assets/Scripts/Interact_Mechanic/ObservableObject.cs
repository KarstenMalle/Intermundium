using System.Collections;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(fileName ="ObservableScriptableObject", menuName ="ScriptableObjects/Observable Object")]
public class ObservableObject : ScriptableObject
{
    //Our current Observable object properties
    [SerializeField]    private bool isObservable = false;
    [SerializeField]    private bool isReadable = false;
    [SerializeField]    private bool isSolvable = false;
    [SerializeField]    private bool isAnimationTrigger = false;
    
    [SerializeField]    private string text;
    [SerializeField]    private AudioSource audioSource;
    [SerializeField]    private GameObject observableobject;
    [SerializeField]    private Animator AnimateOnTrigger;


    private bool resetObservable;
    private bool resetReadable;
    private bool resetSolvable;
    private bool resetAnimationTrigger;

    private void Awake()
    {
        resetObservable = isObservable;
        resetReadable = isReadable;
        resetSolvable = isSolvable;
        resetAnimationTrigger = isAnimationTrigger;
    }

    //Getters
    public bool GetIsObservable(){
        return isObservable;
    }
    public bool GetIsAniTrigger(){ 
        return isAnimationTrigger;
    }
    public bool GetIsSolvable(){
        return isSolvable;
    }
    public bool GetIsAnimationTrigger(){
        return isObservable;
    }

    //Setters
    public void SetIsObservable(bool val){
        isObservable = val;
    }
    public void SetIsReadable(bool val)
    {
        isReadable = val;
    }
    public void SetIsSolvable(bool val)
    {
        isSolvable = val;
    }
    public void SetIsAnimationTrigger(bool val)
    {
        isAnimationTrigger = val;
}


    //Functions
    public string GetText(){
        return text;
    }
    public GameObject GetObject()
    {
        return observableobject;
    }
    public Animator GetAnimate()
    {
        return AnimateOnTrigger;
    }
    public AudioSource GetAudioSrc()
    {
        return audioSource;
    }


    public void resetObservableObject()
    {
        isObservable = resetObservable;
        isReadable = resetReadable;
        isSolvable = resetSolvable;
        isAnimationTrigger = resetAnimationTrigger;

    }

}

