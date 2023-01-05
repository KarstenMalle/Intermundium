using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BookScript : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;

    public GameObject canvasScreen;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    void Update()
    {
        if (_ePressed)
        {
            Debug.Log("TEST Loading screen");
            StopAllCoroutines();
            StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
    public string InteractionPrompt => _promt;
    public bool Interact(Interactor interactor)
    {
        _ePressed = true;

        return true;
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        canvasScreen.SetActive(true);
        loadingScreen.SetActive(true);

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
