using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Finish : MonoBehaviour
{
    public GameObject loadingScreen;

    void Start()
    {
        loadingScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "First Person Controller")
        {
            //ADD LOADINGBAR SCENE
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
