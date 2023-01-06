using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider VolumeSlider;
    public static float VolumeLevel;


    void Start()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.fullScreen = true;
        VolumeLevel = 1;
    } 

    public void PlayGame ()
    {
        // File > Build Settings... Make sure Scenes/start_menu has index 0 and Scenes/Level01 has index 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("EndCredits");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        //Make exit button work in Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #endif
        Application.Quit();
    }
}
