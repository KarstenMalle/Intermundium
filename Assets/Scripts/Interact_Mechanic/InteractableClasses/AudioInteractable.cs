using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public abstract class AudioInteractable : Interactable
{

    [SerializeField] protected ScriptObject_AudioInteractable ObjectInfo;
    [SerializeField][Range(0.0f, 1.0f)] protected float Volume;

    protected AudioClip audioclip = null;

    protected AudioSource audioSource = null;
    protected Animator animator = null;
    protected float audioLength;
    protected bool isPlaying = false;

    /// <summary>
    /// This is a normal unity Awake. 
    /// One important difference is that its virtual and therefore callable in the subclass. 
    /// This allows alot of code to be abstracted away from the individual subclasses
    /// </summary>
    /// <exception cref="FileLoadException"></exception>
    public virtual void Awake()
    {
        //Fetch the Scriptable Object attached to the Gameobject this is attached to.
        audioclip = ObjectInfo.audioClip;

        if (audioclip == null)
        {
            Debug.LogWarning("Add missing Audio Clip");
            throw new FileLoadException("Audio Clip for Cassette Tape is missing");
        }

        if (audioSource == null || animator == null)
        {
            //Single instance of audiosource pr. Cassette Tape Player
            audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.clip = audioclip;

            audioSource.volume = Volume;

            audioLength = audioSource.clip.length;

            //Single instance of an animator pr. Cassette Tape Player
            animator = gameObject.AddComponent<Animator>();
            //yet to figure out how animator functions.

        }
    }

    public override bool GetIsInteractable(){ return ObjectInfo.isInteractable; }

    protected abstract IEnumerator PlayAudio();
    protected abstract IEnumerator PlayAnimation();
}