using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseCanvas;

    float tmp_sensitivity = 0f;

    public static float mute = 1f;

    void Start()
    {
        PauseCanvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        CheckStatus();
    }
    
    public void CheckStatus()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonLook.sensitivity = tmp_sensitivity;
        GameIsPaused = false;
        mute = 1f;
    }

    public void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        tmp_sensitivity = FirstPersonLook.sensitivity;
        FirstPersonLook.sensitivity = 0f;
        GameIsPaused = true;
        mute = 0f;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        //Make exit button work in Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #endif
        Application.Quit();
    }

    public void gotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
