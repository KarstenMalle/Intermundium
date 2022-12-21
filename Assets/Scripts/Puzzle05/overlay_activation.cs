using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlay_activation : MonoBehaviour
{
    public GameObject[] overlay;

    [SerializeField]
    static public bool have_pages = false;

    private bool overlay_active = false;
    private bool just_pressed = false;

    static public bool entering_code = false;

    // Update is called once per frame
    void Update()
    {
        if (have_pages && !entering_code) {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !overlay_active) {
                overlay[0].SetActive(true);
                overlay_active = true;
                just_pressed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !overlay_active) {
                overlay[1].SetActive(true);
                overlay_active = true;
                just_pressed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && !overlay_active) {
                overlay[2].SetActive(true);
                overlay_active = true;
                just_pressed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && !overlay_active) {
                overlay[3].SetActive(true);
                overlay_active = true;
                just_pressed = true;
            }



            //if 1,2,3,4 or q are pressed and overlay_active is true, disable all overlays
            if ((Input.GetKeyDown(KeyCode.Alpha1) || 
                Input.GetKeyDown(KeyCode.Alpha2) || 
                Input.GetKeyDown(KeyCode.Alpha3) || 
                Input.GetKeyDown(KeyCode.Alpha4) || 
                Input.GetKeyDown(KeyCode.Q)) 
                && overlay_active && !just_pressed) {
                foreach (GameObject o in overlay)
                {
                    o.SetActive(false);
                }
                overlay_active = false;
            }

            if (just_pressed) {
                just_pressed = false;
            }
        }
    }
}
