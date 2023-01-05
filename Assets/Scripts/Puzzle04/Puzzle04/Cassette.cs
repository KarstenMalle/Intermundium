using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassette : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    private bool _ePressed = false;

    static public bool cassetteCollected;
    [SerializeField] private IntSO cassettesSO;
    public GameObject cassette1Object;
    public GameObject cassette2Object;
    public GameObject cassette3Object;
    public GameObject cassette4Object;
    // Start is called before the first frame update
    void Start()
    {
        cassette1Object.SetActive(true);

        cassette2Object.SetActive(false);
        cassette3Object.SetActive(false);
        cassette4Object.SetActive(false);

        cassettesSO.Value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_ePressed)
        {
            if(cassettesSO.Value == 0)
            {
                cassettesSO.Value += 1;
                cassette1Object.SetActive(false);
                cassette2Object.SetActive(true);
            }
            else if(cassettesSO.Value == 1)
            {
                cassettesSO.Value += 1;
                cassette2Object.SetActive(false);
                cassette3Object.SetActive(true);
            }
            else if(cassettesSO.Value == 2)
            {
                cassettesSO.Value += 1;
                cassette3Object.SetActive(false);
                cassette4Object.SetActive(true);
            }
            else if(cassettesSO.Value == 3)
            {
                cassettesSO.Value += 1;
                cassette4Object.SetActive(false);
                Debug.Log("Collected all!");
            }
            else
            {
                Debug.Log("ERROR");
                cassettesSO.Value = 0;
            }
            
        }

    }

    public string InteractionPrompt => _promt;
    public bool Interact(Interactor interactor)
    {
        _ePressed = true;

        return true;
    }
}
