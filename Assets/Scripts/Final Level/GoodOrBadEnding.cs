using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodOrBadEnding : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;

    [SerializeField] private GameObject BadEndingObject;
    [SerializeField] private GameObject WhitePlaneObject;
    [SerializeField] private GameObject DarkPlaneObject;
    [SerializeField] private IntSO pagesSO;

    private float deltaT = 0;

    // Start is called before the first frame update
    void Start()
    {
        BadEndingObject.SetActive(false);
        WhitePlaneObject.SetActive(true);
        DarkPlaneObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_ePressed)
        {
            deltaT += Time.deltaTime;
            Debug.Log(pagesSO.Value);
            if (pagesSO.Value == 5)
            {
                Debug.Log("Good ending");
                StopAllCoroutines();
                SceneManager.LoadScene("GoodEnding");
            }
            else
            {
                
                Debug.Log("Bad ending");
                WhitePlaneObject.SetActive(false);
                DarkPlaneObject.SetActive(true);
                BadEndingObject.SetActive(true);
            }
            if (deltaT > 8 && pagesSO.Value != 5)
            {
                SceneManager.LoadScene("EndCredits");
            }
        }
        
    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        _ePressed = true;
        //Update();

        Debug.Log("U pressed the page2");


        return true;
    }
}
