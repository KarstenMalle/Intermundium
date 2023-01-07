using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class Finish : MonoBehaviour
{
    public GameObject canvasScreen;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public Camera firstPersonCam;
    public Camera loadingScreenCam;
    public GameObject loadingScreenParent;

    public Sprite[] bgImageList;
    private List<int> usedImgID;

    private float deltaT = 0;
    private bool enableLoadScreen = false;
    private bool callOnce;

    private float fakeProgress = 0;

    private void Awake()
    {
        firstPersonCam.enabled = true;
        loadingScreenCam.enabled = false;
        loadingScreenParent.SetActive(false);
    }

    private void Start()
    {
        callOnce = false;
        usedImgID = new List<int>();
    }

    private void Update()
    {
        if(enableLoadScreen)
        {
            if (!callOnce)
            {
                changeBackgroundImage();
                //keepAspectRatio();
                callOnce = true;
                loadingScreenParent.SetActive(true);
                canvasScreen.SetActive(true);
                loadingScreen.SetActive(true);
                firstPersonCam.enabled = false;
                loadingScreenCam.enabled = true;
            }

            deltaT += Time.deltaTime;
            if (deltaT > 3)
            {
                Debug.Log("TEST Loading screen");
                StopAllCoroutines();
                StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
            }
            else
            {
                fakeProgress = deltaT / 3;
                float rounded_progress = Mathf.Round(fakeProgress * 100f) / 100f;
                slider.value = rounded_progress;
                progressText.text = rounded_progress * 100f + "%";
            }
        }
        
    }

    void keepAspectRatio()
    {
        var topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var worldSpaceWidth = topRightCorner.x * 2;
        var worldSpaceHeight = topRightCorner.y * 2;

        var spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;

        var scaleFactorX = worldSpaceWidth / spriteSize.x;
        var scaleFactorY = worldSpaceHeight / spriteSize.y;

        if (scaleFactorX > scaleFactorY)
        {
            scaleFactorY = scaleFactorX;
        }
        else
        {
            scaleFactorX = scaleFactorY;
        }
        gameObject.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1);
    }

    int randomNum()
    {
        int num = Random.Range(0, bgImageList.Length);
        return num;
    }

    void changeBackgroundImage()
    {
        int selectedID = randomNum();
        bool uniqueImg = true;
        foreach (int usedID in usedImgID)
        {
            Debug.Log("Selected ID: " + selectedID + " Used ID: " + usedID);
            if(selectedID == usedID)
            {
                uniqueImg = false;
            }
        }
        if(uniqueImg)
        {
            loadingScreen.GetComponent<Image>().sprite = bgImageList[selectedID];
            usedImgID.Add(selectedID);
        }
        else
        {
            changeBackgroundImage();
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "First Person Controller")
        {
            enableLoadScreen = true;
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        yield return null;
        /*
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        */
    }
}
