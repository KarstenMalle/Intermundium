using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodEnding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "First Person Controller")
        {
            Debug.Log("TEST Loading screen");
            StopAllCoroutines();
            SceneManager.LoadScene("EndCredits");
        }
    }


}
