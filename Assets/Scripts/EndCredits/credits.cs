using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class credits : MonoBehaviour
{

    [SerializeField] private Animator animator;

    void Start()
    {
        animator.Play("CreditAnimation");    
    }

    private void Update()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            StopAllCoroutines();
            StartCoroutine(LoadAsynchronously(0));
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
