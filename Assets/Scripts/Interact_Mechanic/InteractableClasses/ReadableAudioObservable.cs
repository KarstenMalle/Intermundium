using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ReadableAudioObservable : Interactable
{

    [SerializeField] protected ScriptObject_ReadableAudioObservable ObjectInfo;
    [SerializeField][Range(0.0f, 1.0f)] protected float Volume;

    private void OnGUI()
    {
        Volume = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), Volume, 0f, 100f);
    }

    protected AudioClip audioclip = null;
    protected AudioSource audioSource = null;
    protected float audioLength = 0;


    protected virtual void Awake()
    {
        //Fetch the Scriptable Object attached to the Gameobject this is attached to.
        audioclip = ObjectInfo.audioClip;

        if (audioclip == null)
        {
            Debug.LogWarning("Add missing Audio Clip");
            throw new FileLoadException("Audio Clip for Cassette Tape is missing");
        }

        if (audioSource == null)
        {
            //Single instance of audiosource pr. Cassette Tape Player
            audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.clip = audioclip;

            audioLength = audioSource.clip.length;
        }
    }


    //Property Getters
    public override bool GetIsObservable() { return ObjectInfo.isObservable; }
    public override bool GetIsReadableText() { return ObjectInfo.isReadable; }
    public override string GetText() { return ObjectInfo.text;  }

    protected abstract IEnumerator PlayAudio();

}