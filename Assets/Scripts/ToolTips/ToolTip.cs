using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private GameObject tooltipCanvas;
    public Image myBackground;

    private bool TooltipActive;
    private bool isDone = true;
    float tmp_sensitivity = 0f;
    public Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        tooltipCanvas.SetActive(false);
        myBackground.GetComponent<Image>().color = Color.clear;
        TooltipActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && isDone)
        {
            if(!TooltipActive)
            {   
                Debug.Log("Show tooltip");
                Pause();
                StartCoroutine(FadeInBackground(Color.clear, Color.black, 2f));
                tooltipCanvas.SetActive(true);
                
            }
            else if(TooltipActive)
            {
                Debug.Log("Hide tooltip");
                Resume();
                StartCoroutine(FadeInBackground(Color.black, Color.clear, 2f));
            }
        }


    }
    IEnumerator FadeInBackground(Color start, Color end, float duration)
    {
        isDone = false;
        if (TooltipActive)
        {
            tooltipCanvas.SetActive(false);
        }
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            myBackground.GetComponent<Image>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        myBackground.GetComponent<Image>().color = end; //without this, the value will end at something like 0.9992367
        TooltipActive = !TooltipActive;
        isDone = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonLook.sensitivity = tmp_sensitivity;
        playerRb.isKinematic = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        tmp_sensitivity = FirstPersonLook.sensitivity;
        FirstPersonLook.sensitivity = 0f;
        playerRb.isKinematic = true;
    }
}
