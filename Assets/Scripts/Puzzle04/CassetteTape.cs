using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteTape : MonoBehaviour
{
    [SerializeField]private Animation animator = null;
    [SerializeField]public new Animator animation = null;

    private void Awake()
    {
        if(animator == null || animation == null)
        {
            Debug.LogWarning("No Animator or Animation is added to the CassetteTape Object {gameObject.name}");
        }
    }

    public void PlayCassetteTape(float audioClipLength)
    {
        StartCoroutine(CassetteTapeAnimation(audioClipLength));
    }

    private IEnumerator CassetteTapeAnimation(float audioClipLength)
    {
        animator.Play("PlayCassetteTape");
        yield return new WaitForSecondsRealtime(audioClipLength);
    }

}
