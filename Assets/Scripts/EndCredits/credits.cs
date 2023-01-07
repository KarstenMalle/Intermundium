using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class credits : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private IntSO pagesSO;
    [SerializeField] public TextMeshProUGUI pagesText;

    private GameObject endingSpeaker;

    void Start()
    {
        pagesText.text = "Thanks for playing!\n" + "You have collected\n" + pagesSO.Value + "/5 pages.";
        animator.Play("CreditAnimation");
        endingSpeaker = GameObject.FindGameObjectWithTag("EndingMusic");
    }

    private void Update()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Destroy(endingSpeaker);
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
