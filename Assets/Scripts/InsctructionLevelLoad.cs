using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
//using System.Diagnostics;

public class InsctructionLevelLoad : MonoBehaviour
{
    public GameObject panelScreen;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    

    private void Start()
    {
        panelScreen.SetActive(true);
        loadingScreen.SetActive(false);
    }
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StopAllCoroutines();
            StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        panelScreen.SetActive(false);
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
   
            yield return null;
        }
    }
}



