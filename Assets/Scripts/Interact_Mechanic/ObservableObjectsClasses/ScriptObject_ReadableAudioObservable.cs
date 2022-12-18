using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptObject_ReadableAudioObservable", menuName = "ScriptableObjects/Readable Audio Observable Script Object")]
public class ScriptObject_ReadableAudioObservable : ScriptableObject, IReadable, IObservable, IsAudible
{
    //Our current Observable object properties
    [SerializeField] public bool isObservable = false;
    [SerializeField] public bool isReadable = false;
    [SerializeField] public bool isAudible = false;

    [SerializeField] public AudioClip audioClip;
    [SerializeField][TextArea(15, 20)] public string text;

    bool resetAudible;
    bool resetReadable;
    bool resetObservable;

    
    //IObservable
    bool IObservable.IsObservable { get => isObservable; set => isObservable = value; }

    //IReadable
    bool IReadable.IsReadable { get => isReadable; set => isReadable = value; }
    string IReadable.text { get => text; set => text = value; }

    //IAudible
    AudioClip IsAudible.audioClip => audioClip;
    bool IsAudible.IsAudible { get => isAudible; set => isAudible = value; }

    
    private void Awake()
    {
        resetAudible = isAudible;
        resetReadable = isReadable;
        resetObservable = isObservable;
    }

    public void resetAudioObservableObject()
    {
        isAudible = resetAudible;
        isReadable = resetReadable;
        isObservable = resetObservable;

    }
    


}
 