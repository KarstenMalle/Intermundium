using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CassettePlayer : AudioInteractable
{
    public override void Awake() {
        base.Awake();
    }

    /// <summary>
    /// This functions need to contain everything in the correct sequential order for the interact engine to work. 
    /// The interact engine only looks for the PerformInteraction function, so its very much them main() in any interaction classes.
    /// </summary>
    public override void PerformInteraction()
    {
        //Start playing audio and animation at the same time
        StartCoroutine(PlayAudio());
        StartCoroutine(PlayAnimation());

        //Coroutine for the cassette tape animation inside the cassette player
        //cassetteTape.PlayCassetteTape(audioLength);
    }

    /// <summary>
    /// Coroutine that plays audio, inherited from AudioInteractable
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator PlayAudio()
    {
        if (audioSource.isPlaying == true)
        {

            StopCoroutine(PlayAudio()); //Stop the current coroutine, if an instance is already playing.
        }

        audioSource.Play();
        yield return new WaitForSeconds(audioLength); //Continue till finished
    }

    /// <summary>
    /// Coroutine that plays animator, inherited from AudioInteractable
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator PlayAnimation()
    {
        //animator.Play("CassettePlay");
        yield return new WaitForSeconds(audioLength);
    }

    
}
