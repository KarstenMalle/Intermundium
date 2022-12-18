using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPaper : ReadableAudioObservable
{
    public override void PerformInteraction() {
        StartCoroutine(PlayAudio()); 
    }

    protected override IEnumerator PlayAudio()
    {
        if (audioSource.isPlaying == true)
        {

            StopCoroutine(PlayAudio()); //Stop the current coroutine, if an instance is already playing.
        }

        audioSource.Play();
        yield return new WaitForSeconds(audioLength); //Continue till finished
    }
}
