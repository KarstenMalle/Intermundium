using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstPage : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;

    public GameObject toolTip;
    private GameObject player;
    public GameObject pageObject;

    [SerializeField] private GameObject canvas;

    [SerializeField] private IntSO pagesSO;
    [SerializeField] public TextMeshProUGUI pagesText;
    [SerializeField] private GameObject pagesCanvas;
    static public bool pageCollected;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        toolTip.SetActive(false);
        canvas.SetActive(false);
        pagesCanvas.SetActive(false);
        pageCollected = false;
        pageObject.SetActive(true);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, pageObject.transform.position) < (2 * 1) && _ePressed == false)
        {
            //Debug.Log((player.transform.position - this.transform.position).sqrMagnitude);
            toolTip.SetActive(true);
        }
        else
        {
            toolTip.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            canvas.SetActive(false);
            pageObject.SetActive(false);
        }
        if (_ePressed && !pageCollected)
        {
            pagesSO.Value += 1;
            pagesText.text = pagesSO.Value + "/5";
            pagesCanvas.SetActive(true);
            pageCollected = true;
        }
    }

    /*
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - pagesText.text.Length, 10, pagesText.text.Length, 22), pagesText.text);
    }
    

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = (int)(20.0f * ((float)Screen.width / (float)nativeSize.x));
        GUI.Label(new Rect(Screen.width - 40, 10, 200, 200), pagesText.text, style);
    }
    */

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        canvas.SetActive(true);
        _ePressed = true;
        //Update();

        Debug.Log("U pressed the page2");
        

        return true;
    }
}
