using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FifthPage : MonoBehaviour, IInteractable
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
    private bool pageCollected;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        toolTip.SetActive(false);
        canvas.SetActive(false);
        pagesCanvas.SetActive(false);
        pageCollected = false;
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
        }
        if (_ePressed && !pageCollected)
        {
            pagesSO.Value += 1;
            pagesText.text = pagesSO.Value + "/5";
         
            pagesCanvas.SetActive(true);
            pageCollected = true;
        }
    }

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
