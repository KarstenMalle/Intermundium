using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;

public class InspectEngine : MonoBehaviour
{
    //Public stuff, let it fly, idc.
    [SerializeField] public GameObject Player; //Player 
    [SerializeField] public const float MaxLength = 5;  //5 unity units.
    [SerializeField] public GameObject Inspect_UI; //Inspect Canvas
    [SerializeField] public GameObject Read_UI; //Read Panel Containing everything to display text
    [SerializeField] public Camera MainCamera; //Maincamera
    //[SerializeField] public Camera InspectCamera; //Secondary inspect camera 
    [SerializeField] public KeyCode ReadKey;
    [SerializeField] public KeyCode ObserveKey;
    [SerializeField] public float RotationSpeed;
    [SerializeField] public float ScrollSpeed;


    //Debug Stuff
    [SerializeField] public KeyCode DebugKey; //Used to get a single debug output
    [SerializeField] public bool EnableDebugMode = false; //enable or disable

    //What they do? i don't remember
    //public string UITargetLayer;
    public Vector3 DefaultInspectCameraRotation;

    //Private stuff, lets keep this shit hidden.
    private Ray mainCamDir;
    private RaycastHit hit;
    private GameObject inspect_gameobject;
    private GameObject Read_TextField_UI;
    private TextMeshProUGUI Read_TextField_text_UI;
    private Quaternion curr_rot;
    private Collider inst_collider;
    private int layermask = 1 << 8; //layermask to give to a new object (layer is Interactables)
    private bool reading = false;
    private bool observing = false;

    //Variables specific to the object we can interact with, where we can fetch information from.
    private Transform object_Hit;
    private GameObject inst_inspect_object = null;
    private Interactable _interactable = null;

    public static string object_Hit_Name{ get; private set; }


    /// <summary>
    /// Returns the name which we are currently looking at.
    /// </summary>
    /// <returns name="object_Hit_Name"></returns>
    public static string GetObjectLookingAtCurrently()
    {
        return object_Hit_Name;
    }

    private void debugMode()
    {
        Debug.Log(string.Format("Currently Looking at: {0}", GetObjectLookingAtCurrently()));
        Debug.Log(string.Format("Current Properties - Interactable: {0} | Observable: {1} | Readable {2}", _interactable.GetIsInteractable(), _interactable.GetIsObservable(), _interactable.GetIsReadableText() ));
    }

    void Awake()
    {
        //InspectCamera.enabled = false;
        Inspect_UI.SetActive(false);

        foreach (Transform element in Read_UI.transform)
        {
            
            if (element.name == "UI_TextContainer") //the container which holds the textfield object must be named "UI_TextContainer"
            {
                Read_TextField_UI = element.GetChild(0).gameObject; //Store the element, The gameobject containing text MUST ALWAYS BE THE FIRST CHILD OBJECT.
                Read_TextField_text_UI = element.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                Debug.Log(Read_TextField_UI.name);
            }
        }

       

        if (Read_TextField_UI == null)
        {
            Debug.LogError("Couldn't find UI_TextContainer under Read UI");
        }

        

    }

    //-------------- Current issues --------------------
    //1. unable to lock camera - still a fucking issue
    //2. On trigger collider fucks up.

    // Update is called once per frame
    void Update()
    {
        if (IsObjectInteractable() || observing == true)
        {
            if(observing == true)
            {
                LockCameraRotation();
            }
            else
            {
                UnLockCameraRotation();
            }

            Check_Interaction();
        }

        if(EnableDebugMode == true){ if(Input.GetKeyUp(DebugKey)) { debugMode();} };
    }

    /*
     * We know that if the object doens't have the Observation boolean value to true its a basic interactions
     * Therefore run checks on Observation, in the case its not an observable object we dont care about anything and just run, RunBasicInteraction().
     */

    //-------------------------- Functions for handling observation and interactions -----------------------------------------

    /// <summary> 
    /// Check if an interaction is taking place
    /// if its an observation stop observating however if its a basic interaction wait till finished, to ensure proper execution
    /// 
    /// </summary>
    /// <returns></returns>
    void Check_Interaction()
    {
        Check_IfObservationOrBasicInteraction();

        return;
    }


    void LockCameraRotation()
    {
        //MainCamera.gameObject.transform.localRotation.x
        return;
    }
    void UnLockCameraRotation()
    {
        MainCamera.gameObject.isStatic = true;
    }

    /// <summary>
    /// either RunBasicInteraction or EnableActiveObservation
    /// </summary>
    void Check_IfObservationOrBasicInteraction()
    {
        if (_interactable.GetIsInteractable()) // add interactable properties here 
        {
            if (Input.GetKeyUp(ObserveKey) && !_interactable.GetIsReadableText())
            {
                RunBasicInteraction();
            }
            else if(Input.GetKeyUp(ObserveKey) && _interactable.GetIsReadableText() && reading == false)
            {
                reading = true;
                RunBasicInteraction(); //We may have an audio or animation we want to run first
                Enable_ActiveReading();  
            }
            else if(Input.GetKeyUp(ObserveKey) && _interactable.GetIsReadableText() && reading == true)
            {
                reading = false;
                Disable_ActiveReading();
            }
        }
        else if(_interactable.GetIsObservable()) //add observable properties here
        {
            if (_interactable.GetIsAudible() /* || any other property in observable like GetIsSolvable*/)
            {

                //if we have any property we want to use it.
                RunBasicInteraction();
                determine_ActiveObservation(ObserveKey, ReadKey);
            }
            else
            {
                determine_ActiveObservation(ObserveKey, ReadKey);
            }
        }
    }


    /// <summary>
    /// Checks if we have a readable property on our interactable if the specified keyboard key have been pressed
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool Check_ReadableText(KeyCode key)
    {
        if (Input.GetKeyUp(key))
            if (_interactable.GetIsReadableText())
            {
                return true;
            }
    
        return false;
    }

    /// <summary>
    /// Determines whether we are to observe or not observe the object.
    /// Based on whether a keypress have been made
    /// </summary>
    /// <param name="Observe, Read" ></param>
    /// <returns></returns>
    private bool determine_ActiveObservation(KeyCode Observe, KeyCode Read)
    {
        if (Input.GetKeyUp(Observe))
        {
            curr_rot = Player.transform.rotation; //store current player rotation.

            if (observing == true && Input.GetKeyUp(Observe))
            {
                observing = false;
                Disable_ActiveObservation();
            }
            else if (observing == false)
            {
                observing = true;
                Enable_ActiveObservation();
            }
        }

        //We are observing, we are not currently reading and we want to check if the object have the readable property 
        else if (observing == true && reading == false && Check_ReadableText(Read)) 
        {
            reading = true;
            Enable_ActiveReading();
        }
        else if (observing == true && reading == true && Check_ReadableText(Read))
        {
            reading = false;
            Disable_ActiveReading();
        }

        return false;
    }

    void Enable_ActiveReading()
    {

        Read_TextField_text_UI.text = _interactable.GetText();
        if(Inspect_UI.activeSelf == false)
        {
            Inspect_UI.SetActive(true);
        }
        Read_UI.SetActive(true);

        PauseGame();
    }

    void Disable_ActiveReading()
    {
        Read_UI.SetActive(false);

        //if the object is observable we down want to close the inspect UI
        if (_interactable.GetIsObservable())
        {
            return;
        }
        else
        {
            Inspect_UI.SetActive(false);
        }
        ResumeGame();
    }


    void Disable_ActiveObservation()
    {
        Debug.Log("Stop observing");
        Inspect_UI.SetActive(false);
        //InspectCamera.enabled = false;
        //InspectCamera.transform.position = DefaultInspectCameraRotation;
        //InspectCamera.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));
        observing = false;
        HideMouseCursor();
        Destroy(inst_inspect_object);
        ResumeGame();
    }

    void Enable_ActiveObservation()
    {
        //MainCamera.transform.rotation = objectHit.localRotation;
        //InspectCamera.transform.position = objectHit.transform.localPosition;
        //InspectCamera.transform.rotation = objectHit.transform.localRotation;
        //instansiate object for UI visibility. and ensure only a single observable object can be used.
        //inspect_gameobject = objectHit.transform.gameObject;
        //ScriptableObject inspect_gameobject_values = inspect_gameobject.GetComponent<ScriptableObject>();
        
        PauseGame();

        Debug.Log("start observing");
        //InspectCamera.enabled = true;

        //Enable UI and ObservationTracking
        observing = true;
        Inspect_UI.SetActive(true);
        ShowMouseCursor();

        //Creating the observable object as an instantiated object
        inst_inspect_object = Instantiate(inspect_gameobject, Inspect_UI.transform.position, transform.rotation);

        Debug.Log(inst_inspect_object);

        //fix is Trigger bug
        inst_collider = inst_inspect_object.GetComponent<Collider>();
        inst_collider.enabled = false;
        inst_collider.isTrigger = true;
        inst_collider.enabled = true;


        inst_inspect_object.transform.SetParent(GameObject.FindGameObjectWithTag("Inspect_GUI").transform, false);
        inst_inspect_object.transform.localScale += new Vector3(100f, 100f, 100f);
        inst_inspect_object.transform.localPosition = Camera.main.transform.position + Camera.main.transform.forward * MaxLength;
        inst_inspect_object.transform.localPosition = new Vector3(inst_inspect_object.transform.localPosition.x, inst_inspect_object.transform.localPosition.y, inst_inspect_object.transform.localPosition.z - 200);
        inst_inspect_object.AddComponent<RotatorNZoomer>();
        inst_inspect_object.layer = LayerMask.NameToLayer("UI");

        //Rotate and scroll values
        inst_inspect_object.GetComponent<RotatorNZoomer>().InspectCamera = MainCamera;
        inst_inspect_object.GetComponent<RotatorNZoomer>().ScrollSpeed = ScrollSpeed;
        inst_inspect_object.GetComponent<RotatorNZoomer>().RotationSpeed = RotationSpeed;

        //If the instantiated inspectable gameobject have children, change those to be on the UI layer 
        if (inst_inspect_object.transform.childCount > 0)
        {
            foreach (Transform child in inst_inspect_object.GetComponentInChildren<Transform>(includeInactive: true))
            {
                child.gameObject.layer = LayerMask.NameToLayer("UI");
            }
        }

        inst_inspect_object.layer = LayerMask.NameToLayer("UI");
    }


    /// <summary>
    /// Sets Time.timescale equal to 0
    /// </summary>
    private void PauseGame() { Time.timeScale = 0; }


    /// <summary>
    /// Sets Time.timescale equal to 1
    /// </summary>
    private void ResumeGame() { Time.timeScale = 1; }


    /// <summary>
    /// Shows the mouse cursor
    /// </summary>
    private void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    /// <summary>
    /// Hides mousecursor such thats its not visible 
    /// outside of the Inspect observable object GUI
    /// </summary>
    private void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    /// <summary>
    /// Runs The Basic interaction that the interactable object has.
    /// </summary>
    /// <param name="BasicInteractionObject"></param>
    private void RunBasicInteraction() {
        Debug.Log("Basic Interaction was performed");
        _interactable.PerformInteraction(); }


    /// <summary>
    /// Checks if the raycast finds anythin on the layermask with the MaxlengthRange
    /// </summary>
    /// <returns>Bool</returns>
    bool IsObjectInteractable()
    {
        mainCamDir = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mainCamDir, out hit, MaxLength, layermask)){
            object_Hit = hit.transform; //Get the transform
            object_Hit_Name = object_Hit.name;

            inspect_gameobject = object_Hit.transform.gameObject; //Gameobject we are hitting with the raycast
            _interactable = object_Hit.GetComponent<Interactable>();


            if (inspect_gameobject = null) { Debug.LogError("Impossible edge case error has occured, inspectable gameobject doens't exist"); }


            return true;
        }
        else { return false; }
    }
}


