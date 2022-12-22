using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Well, needs to contain whatever interaction\s needs to done.
    public virtual void PerformInteraction() { return; }

    //Property Getters
    public virtual bool GetIsInteractable(){ return false; }
    public virtual bool GetIsObservable(){ return false; }
    public virtual bool GetIsReadableText(){ return false; }
    public virtual bool GetIsAudible() { return false; }
    public virtual string GetText(){ return string.Empty; }
    

    //If an object is solvable.
    public virtual Array GetSolutionArray() { return new Array[0]; }
    public virtual string GetSolutionString() { return string.Empty; }

   

    //protected abstract void FetchScriptableObjectLocal();

}
